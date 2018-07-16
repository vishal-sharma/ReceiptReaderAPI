using ReceiptReaderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReceiptReaderAPI.Services
{
    public class ExpenseRegexService
    {
        private static readonly string amountPattern = @"\$([0-9]+)(.[0-9][0-9])?";
        private static readonly string datePattern = @"(\d{1,2}).(\d{1,2}).(\d{4})";


        public static Expense ExtractExpenseInfo(string input)
        {
            var amountMatch = Regex.Match(input, amountPattern);
            var dateMatch = Regex.Match(input, datePattern);

            return new Expense
            {
                amount = amountMatch.Value,
                date = dateMatch.Value
            };
        }
    }
}
