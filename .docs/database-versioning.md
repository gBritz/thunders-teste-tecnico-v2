# Controle de Versão de Banco de Dados com Entity Framework

O Entity Framework (EF) fornece um mecanismo robusto para controle de versão de banco de dados, garantindo que o esquema do banco permaneça consistente com os modelos da aplicação. Veja como gerenciar o versionamento do banco de dados usando o EF.

## 1. Entendendo as Migrações
As migrações no EF permitem atualizar incrementalmente o esquema do banco de dados para mantê-lo sincronizado com os modelos de dados da aplicação.

### Conceitos-chave:
- **Adicionar Migração:** Cria um script de migração com base nas alterações do modelo.
- **Atualizar Banco de Dados:** Aplica migrações pendentes ao banco de dados.
- **Remover Migração:** Reverte a última migração, caso ainda não tenha sido aplicada.

## 2. Configurando as Migrações
Para começar a usar migrações, certifique-se de que o EF Core está instalado no seu projeto.

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```

Depois, habilite as migrações executando:

```bash
dotnet ef migrations add InitialCreate --project Thunders.TechTest.ApiService --no-build --output-dir Data/Migrations
```

Isso cria um arquivo de migração que representa o estado inicial do banco de dados.

Execute todos os comandos na pasta `src`.

## 3. Aplicando Migrações
Para aplicar as migrações mais recentes ao seu banco de dados, execute:

```bash
dotnet ef database update --project Thunders.TechTest.ApiService --no-build
```

Este comando atualiza o banco de dados para o estado mais recente das migrações.

## 4. Gerenciando Migrações
- **Adicionar Nova Migração:**

```bash
dotnet ef migrations add <nome_da_migracao> --project Thunders.TechTest.ApiService --no-build --output-dir Data/Migrations
```

- **Remover a Última Migração (se não aplicada):**

```bash
dotnet ef migrations remove --project Thunders.TechTest.ApiService --no-build --output-dir Data/Migrations
```

- **Listar Migrações Disponíveis:**

```bash
dotnet ef migrations list --project Thunders.TechTest.ApiService --no-build --output-dir Data/Migrations
```

## 5. Boas Práticas
- **Commit das Migrações:** Sempre envie seus arquivos de migração para o controle de versão.
- **Testar Migrações:** Teste as migrações em ambiente de desenvolvimento ou homologação antes de aplicá-las em produção.
- **Manter as Migrações Limpas:** Revise e limpe migrações desnecessárias ou redundantes regularmente.
- **Backup de Dados:** Sempre faça backup do banco antes de aplicar migrações, especialmente em ambientes de produção.

## 6. Lidando com Bancos de Dados em Produção
Ao aplicar migrações em produção, certifique-se de que:
- Os backups do banco de dados estão disponíveis.
- O tempo de inatividade seja minimizado, agendando atualizações fora do horário de pico.
- Testes completos foram realizados para evitar falhas inesperadas.

## 7. Solucionando Problemas Comuns
- **Conflitos de Migração:** Se vários desenvolvedores adicionarem migrações simultaneamente, resolva conflitos reaplicando as migrações.
- **Incompatibilidades de Modelo:** Verifique se os modelos estão corretamente atualizados para refletir as alterações no banco.
- **Migrações Ausentes:** Use `dotnet ef migrations list` para verificar as migrações disponíveis.

## Conclusão
O sistema de migrações do Entity Framework simplifica o versionamento de banco de dados ao automatizar alterações no esquema e manter a consistência. Ao seguir boas práticas e utilizar os comandos do EF, você garante um banco de dados estável e versionado durante todo o ciclo de desenvolvimento.

Para mais informações detalhadas, consulte a [documentação oficial do EF Core](https://docs.microsoft.com/en-us/ef/core/).