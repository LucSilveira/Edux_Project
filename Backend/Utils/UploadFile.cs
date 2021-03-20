using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduxProject.Utils
{
    public static class UploadFile
    {
        public static string Local(IFormFile _file)
        {
            //Gera um nome unico para o arquivo concatenando com o tipo dele
            var _nomeArquivo = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(_file.FileName);

            var _caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), @"wwwRoot\Upload\Imagens", _nomeArquivo);

            // passa para nosso repositorio o arquivo para o alocar
            using var _streamImagem = new FileStream(_caminhoArquivo, FileMode.Create);

            //faz uma copia do arquivo inserido no nosso repositorio
            _file.CopyTo(_streamImagem);

            return "http://192.168.56.1:50537/upload/imagens/" + _nomeArquivo;
        }
    }
}
