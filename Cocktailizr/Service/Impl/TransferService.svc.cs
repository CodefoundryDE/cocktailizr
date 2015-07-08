//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.IO;
using System.Security.Permissions;
using System.Threading.Tasks;
using Cocktailizr.Model.Service;
using CocktailizrTypes.Model.Message;

namespace Cocktailizr.Service.Impl
{
    public class TransferService : ITransferService
    {
        #region Properties



        #endregion

        #region Vriables

        private readonly CocktailDbService _cocktailDbService;

        #endregion

        #region Constructor

        public TransferService()
        {
            _cocktailDbService = new CocktailDbService();
        }

        #endregion

        #region Methods

        public async Task<RemoteFileInfo> DownloadFile(DownloadRequest request)
        {
            RemoteFileInfo result = new RemoteFileInfo();
            try
            {
                var cocktail = await _cocktailDbService.GetCocktailById(request.CocktailGuid);
                var ms = new MemoryStream(cocktail.ImageBytes);

                result.CocktailGuid = cocktail.Id;
                result.Length = ms.Length;
                result.FileByteStream = ms;
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
        public async Task UploadFile(RemoteFileInfo request)
        {
            var cocktail = await _cocktailDbService.GetCocktailById(request.CocktailGuid);

            Stream sourceStream = request.FileByteStream;

            await sourceStream.ReadAsync(cocktail.ImageBytes, cocktail.ImageBytes.Length, (int)sourceStream.Length);
            
            sourceStream.Close();

            //ToDo: Update des Cocktails im CocktailDbService
        }

        #endregion
    }
}
