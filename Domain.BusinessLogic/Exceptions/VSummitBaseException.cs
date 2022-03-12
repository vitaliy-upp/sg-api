using System;

namespace Domain.BusinessLogic.Exceptions
{
    public class VSummitBaseException : Exception
    {
        public int ErrorCode { get; private set; }
        public VSummitBaseException()
        { }

        public VSummitBaseException(string message)
            : base(message)
        { }

        public VSummitBaseException(int errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }

    }
}
