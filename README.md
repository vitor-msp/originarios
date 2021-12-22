# Originários
## Projeto Final - Recode Pro 2021
### Squad 41

Desenvolvimento de um site para venda de produtos indígenas.

*Em produção...*

###### Como contribuir
- Para iniciar contribuindo: git clone https://github.com/vitor-msp/originarios.git
- Para atualizar seu repositório local: git pull

Após o repositório local estar atualizado:

1. Edite o arquivo Originarios/Web.config, na linha 13, inserindo o seu hostname após data source
```
data source=VITOR-PC;initial catalog=Originarios;
```
2. Abra o arquivo Originarios.sln com o Visual Studio
3. Para que os pacotes sejam ajustados, execute o comando abaixo no Console do Gerenciador de Pacotes
```
Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r
```
4. Execute a aplicação
