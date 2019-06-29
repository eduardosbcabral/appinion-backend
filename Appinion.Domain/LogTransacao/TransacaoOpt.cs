using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Domain.Enum
{
    public enum TransacaoOpt
    {
        // Usuário
        CADASTRAR_USUARIO,
        ATUALIZAR_DESCRITIVO_USUARIO,
        ATUALIZAR_USERNAME_USUARIO,
        ATUALIZAR_EMAIL_USUARIO,
        ATUALIZAR_FOTO_USUARIO,
        LOGIN,
        ATUALIZAR_SENHA,
        INATIVAR_USUARIO,
        SEGUIR_USUARIO,
        PARAR_SEGUIR_USUARIO,

        // Opinião
        CONCORDAR_NOTICIA,
        DISCORDAR_NOTICIA,

        // Publicação
        CADASTRAR_PUBLICACAO,
        EDITAR_PUBLICACAO,
        INATIVAR_PUBLICACAO,
        COMENTAR_PUBLICACAO,
        RECOMPARTILHAR_PUBLICACAO,
        UPVOTE_PUBLICACAO,
        DOWNVOTE_PUBLICACAO
    }
}
