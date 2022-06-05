using System;

namespace Notes.Application.Common.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : base($"Сущность \"{name}\"  не найдена.")
        {
            
        }
    }
}