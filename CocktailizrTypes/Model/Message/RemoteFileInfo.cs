using System;
using System.IO;
using System.ServiceModel;

namespace CocktailizrTypes.Model.Message
{
    [MessageContract]
    public class RemoteFileInfo : IDisposable
    {
        #region Properties
        [MessageHeader(MustUnderstand = true)]
        public Guid CocktailGuid { get; set; }

        [MessageHeader]
        public long Length { get; set; }

        [MessageBodyMember(Order = 1)]
        public Stream FileByteStream { get; set; }

        #endregion

        #region Variables

        private bool _disposed;

        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            // Free any managed objects here.
            if (disposing)
            {
            }

            // Free any unmanaged objects here.
            _disposed = true;
        }

        ~RemoteFileInfo()
        {
            Dispose(false);
        }
        #endregion
    }
}