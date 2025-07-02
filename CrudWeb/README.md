# Moto Management System - Instruções de Execução

1. **Pré-requisitos:**
   - .NET 8 SDK ou superior instalado
   - PostgreSQL instalado e rodando (ajuste a connection string se necessário)

2. **Restaurar dependências:**
   ```sh
   dotnet restore
   ```

3. **Criar o banco de dados:**
   - Execute o script em `Database/database.sql` no seu PostgreSQL para criar as tabelas necessárias.

4. **Rodar o projeto:**
   ```sh
   dotnet run
   ```
   O projeto estará disponível em: `https://localhost:5001` ou `http://localhost:5000`

5. **Testar a API:**
   - Utilize ferramentas como Postman, Insomnia ou Swagger (`/swagger`) para testar os endpoints.

6. **Observações:**
   - Imagens de CNH são salvas na pasta `Storage/LicenseImages`.
   - Para rodar em ambiente diferente, ajuste as configurações em `appsettings.json`.

