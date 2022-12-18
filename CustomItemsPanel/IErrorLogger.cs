using System;
namespace CustomItemsPanel
{
   public interface IErrorLogger
    {
        void LogError(Exception ex);
    }
}
