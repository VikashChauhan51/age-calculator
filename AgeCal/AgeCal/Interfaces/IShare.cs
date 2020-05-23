using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgeCal.Interfaces
{
    public interface IShare
    {
        Task Show(string title, string message, string filePath);
        Task Show(string title, string content);
    }
}
