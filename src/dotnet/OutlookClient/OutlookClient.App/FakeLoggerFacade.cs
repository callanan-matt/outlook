using System;
using Prism.Logging;

namespace OutlookClient.App
{
    public class FakeLoggerFacade : ILoggerFacade
    {
        public void Log(string message, Category category, Priority priority)
        {
            var categoryPrefix = string.Empty;
            switch (category)
            {
                case Category.Debug:
                    categoryPrefix = "DEBUG! >>> ";
                    break;
                case Category.Info:
                case Category.Warn:
                    categoryPrefix = $"{category.ToString()} : ";
                    break;
                case Category.Exception:
                    categoryPrefix = "ERROR! >>> ";
                    break;
            }
            Console.WriteLine(categoryPrefix + message);
        }
    }
}