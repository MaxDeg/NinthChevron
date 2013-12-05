using System;
using System.Collections.Generic;
using BlueBoxSharp.Data;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Helpers;
using BlueBoxSharp.ComponentModel.DataAnnotations;

namespace TestSandbox.Evoconcept
{
	public class Procedures
	{
	
        /// <summary>
        /// Procedure Parameters:
        /// <para>[IN] p_film_id   int</para>
        /// <para>[IN] p_store_id   int</para>
        /// <para>[OUT] p_film_count   int</para>
        /// </summary>
        public static IEnumerable<DataRecord> FilmInStock(DataContext context, System.Nullable<int> inPFilmId, System.Nullable<int> inPStoreId, System.Nullable<int> outPFilmCount) 
        { 
            return context.ExecuteProcedure("`sakila`.`film_in_stock`", inPFilmId, inPStoreId, outPFilmCount);
        }
	
        /// <summary>
        /// Procedure Parameters:
        /// <para>[IN] p_film_id   int</para>
        /// <para>[IN] p_store_id   int</para>
        /// <para>[OUT] p_film_count   int</para>
        /// </summary>
        public static IEnumerable<DataRecord> FilmNotInStock(DataContext context, System.Nullable<int> inPFilmId, System.Nullable<int> inPStoreId, System.Nullable<int> outPFilmCount) 
        { 
            return context.ExecuteProcedure("`sakila`.`film_not_in_stock`", inPFilmId, inPStoreId, outPFilmCount);
        }
	
        /// <summary>
        /// Procedure Parameters:
        /// <para>[IN] domId   int</para>
        /// <para>[OUT] boxId   int</para>
        /// </summary>
        public static IEnumerable<DataRecord> GetDomBoxId(DataContext context, System.Nullable<int> inDomId, System.Nullable<int> outBoxId) 
        { 
            return context.ExecuteProcedure("`evoconcept`.`get_dom_box_id`", inDomId, outBoxId);
        }
	
        /// <summary>
        /// Procedure Parameters:
        /// </summary>
        public static IEnumerable<DataRecord> NewProcedure(DataContext context) 
        { 
            return context.ExecuteProcedure("`world`.`new_procedure`");
        }
	
        /// <summary>
        /// Procedure Parameters:
        /// <para>[IN] min_monthly_purchases   utinyint</para>
        /// <para>[IN] min_dollar_amount_purchased   udecimal(10)</para>
        /// <para>[OUT] count_rewardees   int</para>
        /// </summary>
        public static IEnumerable<DataRecord> RewardsReport(DataContext context, System.Nullable<byte> inMinMonthlyPurchases, System.Nullable<decimal> inMinDollarAmountPurchased, System.Nullable<int> outCountRewardees) 
        { 
            return context.ExecuteProcedure("`sakila`.`rewards_report`", inMinMonthlyPurchases, inMinDollarAmountPurchased, outCountRewardees);
        }
	}
}
