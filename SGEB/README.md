Sistema de gerenciamento de documentos(Registro de motoristas, frota, fichas..),
criado usando ASP.NET MVC 3, C#, HTML/CSS3, responsive-design.

O sistema é dividido em 3 partes:

1 - Motoristas: onde se pode cadastrar/alterar/excluir dados sobre um motorista da empresa,
    onde estão contidos informações sobre RG, CPF, CNH, Endereço e Contato, incluindo imagens dos respectivos
    documentos.
2 - Veiculos: onde se pode cadastrar/alterar/excluir dados sobre um veiculo especifico,
    onde estão contidos informações do documento do veiculo e seu proprietário, assim como as imagens dos 
    respectivos documentos.
3 - Fichas: pode-se criar registros combinando diferentes motoristas-veiculos, com a finalidade de visualização
    destes dados conjuntos, ou geração de um arquivo PDF(Portable Document Format) que pode ser visualizado no 
    browser ou ser feito o download do mesmo.Há também a opção de enviar este arquivo diretamente para um e-mail
    especificado, assim a geração do arquivo se da 'on-fly'

As configurações podem ser alteradas na página "Configurações", tendo-se controle sobre os dados da empresa.
