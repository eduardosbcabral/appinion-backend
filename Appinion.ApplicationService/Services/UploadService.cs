using Appinion.Domain.Config;
using Appinion.Domain.Entity;
using Appinion.Domain.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Appinion.ApplicationService.Services
{
    public class UploadService
    {
        private IFormFile _arquivo { get; set; }
        private string _caminho { get; set; }
        private string _nomeArquivo { get; set; }
        private TipoArquivo _tipo { get; set; }
        private Usuario _usuario { get; set; }

        private readonly IOptions<UploadSettingsModel> _uploadSettings;

        public UploadService(IOptions<UploadSettingsModel> options)
        {
            _uploadSettings = options;
        }

        private async void CopiarArquivo()
        {
            if (!Directory.Exists(_caminho))
            {
                Directory.CreateDirectory(_caminho);
            }

            using (var stream = new FileStream(Path.Combine(_caminho, _nomeArquivo), FileMode.Create))
            {
                await _arquivo.CopyToAsync(stream);
            }
        }

        private void GerarNome()
        {
            _nomeArquivo = Guid.NewGuid().ToString() + '.' + _arquivo.ContentType.Split('/')[1];
        }

        private void ChecarExtensao()
        {
            foreach (var item in _uploadSettings.Value.ExtensoesNaoPermitidas.Split(';'))
            {
                if (_arquivo.ContentType.Contains(item, StringComparison.CurrentCultureIgnoreCase))
                {
                    throw new Exception("Extensão não permitida");
                }
            }
        }

        private void ChecarTamanho()
        {
            if (_arquivo.Length > Domain.Entity.Arquivo.LimiteUpload)
            {
                throw new Exception("Tamanho limite excedido.");
            }
        }

        public void MontarCaminho()
        {
            if (_tipo.Equals(TipoArquivo.FOTO))
            {
                _caminho = Path.Combine(_uploadSettings.Value.UsuarioFoto, _usuario.Id.ToString());
            }

            if (_tipo.Equals(TipoArquivo.PUBLICACAO))
            {
                _caminho = Path.Combine(_uploadSettings.Value.UsuarioPublicacao, _usuario.Id.ToString());
            }
        }

        private Arquivo MontarArquivo()
        {
            return new Arquivo(_nomeArquivo, _arquivo.Length, _usuario, _tipo);
        }
        
        // Ordem de inicialização
        // 1
        public UploadService Arquivo(IFormFile arquivo)
        {
            _arquivo = arquivo;
            return this;
        }

        // 2
        public UploadService Tipo(TipoArquivo tipo)
        {
            _tipo = tipo;
            return this;
        }

        // 3
        public UploadService Usuario(int id)
        {
            _usuario = new Usuario(id);
            return this;
        }

        // 4
        public Arquivo SalvarArquivo()
        {
            ChecarTamanho();
            ChecarExtensao();
            MontarCaminho();
            GerarNome();
            CopiarArquivo();

            return MontarArquivo();
        }
    }
}
