using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSandbox
{
    public class PropertyChangedTask : Task
    {
        /// <summary>
        /// When overridden in a derived class, executes the task.
        /// </summary>
        /// <returns>
        /// true if the task successfully executed; otherwise, false.
        /// </returns>
        public override bool Execute()
        {
            this.InjectMsil();

            return true;
        }

        /// <summary>
        /// Injects the MSIL.
        /// </summary>
        private void InjectMsil()
        {
            ModuleDefinition module = ModuleDefinition.ReadModule(this.AssemblyPath);

            foreach (TypeDefinition type in module.Types)
            {
                foreach (PropertyDefinition prop in type.Properties)
                {
                    foreach (CustomAttribute attribute in prop.CustomAttributes)
                    {
                        if (attribute.Constructor.DeclaringType.FullName.Contains("PropertyChanged"))
                        {
                            ILProcessor msilWorker = prop.SetMethod.Body.GetILProcessor();

                            Instruction ldarg0 = msilWorker.Create(OpCodes.Ldarg_0);

                            var raisePropertyChangedInfo = typeof(ViewModelBase).GetMethod(
                                "RaisePropertyChanged",
                                BindingFlags.Public |
                                BindingFlags.NonPublic |
                                BindingFlags.Instance, null, new[] { typeof(string) }, null);

                            var raisePropertyChanged = module.Import(raisePropertyChangedInfo);

                            Instruction propertyName = msilWorker.Create(OpCodes.Ldstr, prop.Name);

                            Instruction callRaisePropertyChanged = msilWorker.Create(OpCodes.Callvirt,
                                                                                     raisePropertyChanged);

                            msilWorker.InsertBefore(prop.SetMethod.Body.Instructions[0],
                                                    msilWorker.Create(OpCodes.Nop));

                            msilWorker.InsertBefore(
                                prop.SetMethod.Body.Instructions[prop.SetMethod.Body.Instructions.Count - 1],
                                ldarg0);

                            msilWorker.InsertAfter(ldarg0, propertyName);

                            msilWorker.InsertAfter(propertyName, callRaisePropertyChanged);

                            msilWorker.InsertAfter(callRaisePropertyChanged, msilWorker.Create(OpCodes.Nop));
                        }
                    }

                    module.Write(AssemblyPath);
                }
            }
        }
    }
}
