# 💰 Organizzae - Sistema de Gestão Financeira Pessoal

<div align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-green.svg?style=for-the-badge)

**Sistema completo de gestão financeira pessoal desenvolvido com ASP.NET Core 8, seguindo os princípios de Clean Architecture e Domain-Driven Design (DDD)**

[Características](#-características) •
[Tecnologias](#-tecnologias) •
[Instalação](#-instalação) •
[Uso](#-como-usar) •
[Arquitetura](#-arquitetura) •
[Contribuir](#-contribuindo)

</div>

---

## 📋 Sobre o Projeto

**Organizzae** é uma aplicação web completa para gestão financeira pessoal, permitindo que usuários controlem suas **receitas**, **despesas** e **objetivos financeiros** de forma organizada e intuitiva. O projeto foi desenvolvido com foco em **qualidade de código**, **boas práticas** e **arquitetura limpa**.

### 🎯 Objetivos do Projeto

- ✅ Demonstrar domínio de **Clean Architecture** e **DDD**
- ✅ Aplicar **SOLID** e **Design Patterns**
- ✅ Desenvolver uma aplicação **full-stack** completa
- ✅ Implementar **autenticação** e **autorização** segura
- ✅ Criar uma **UI responsiva** e moderna
- ✅ Seguir as **melhores práticas** do .NET 8

---

## ✨ Características

### 🔐 **Autenticação e Segurança**
- Sistema de login com CPF e senha
- Hash de senhas com **BCrypt**
- Autenticação via **Cookies** (.NET Core Identity)
- Validação de CPF brasileiro
- Proteção contra CSRF

### 💸 **Gestão de Despesas**
- Cadastro de despesas com categorias
- Controle de vencimentos
- Registro de pagamentos
- Status automático (Pendente/Paga/Atrasada)
- Despesas recorrentes (semanal, mensal, anual)

### 💰 **Gestão de Receitas**
- Cadastro de receitas com categorias
- Controle de recebimentos
- Receitas recorrentes
- Categorização flexível

### 🎯 **Objetivos Financeiros**
- Definição de metas financeiras
- Acompanhamento de progresso
- Prazo de conclusão
- Valor acumulado vs meta

### 📊 **Dashboard Analítico**
- Visão geral mensal (receitas, despesas, saldo)
- Gráficos de pizza por categoria (Chart.js)
- Listagem de maiores gastos
- Próximos vencimentos (7 dias)
- Alertas de despesas atrasadas
- Comparação com período anterior

### 📱 **Interface Responsiva**
- Design **mobile-first**
- Compatível com tablets e desktops
- Tema moderno com **Bootstrap 5**
- Máscaras de input (CPF, moeda, data)
- Feedback visual (alertas, modais, loading)

---

## 🚀 Tecnologias

### **Backend**
- [.NET 8](https://dotnet.microsoft.com/) - Framework principal
- [ASP.NET Core MVC 8](https://docs.microsoft.com/aspnet/core/mvc/) - Web framework
- [Entity Framework Core 8](https://docs.microsoft.com/ef/core/) - ORM
- [SQL Server](https://www.microsoft.com/sql-server) - Banco de dados
- [AutoMapper 13](https://automapper.org/) - Mapeamento objeto-objeto
- [FluentValidation 11](https://fluentvalidation.net/) - Validações
- [BCrypt.Net](https://github.com/BcryptNet/bcrypt.net) - Hash de senhas
- [MediatR 12](https://github.com/jbogard/MediatR) - Mediator pattern

### **Frontend**
- [Bootstrap 5.3](https://getbootstrap.com/) - Framework CSS
- [Chart.js 4.4](https://www.chartjs.org/) - Gráficos interativos
- [jQuery 3.7](https://jquery.com/) - Manipulação DOM
- [Font Awesome 6.5](https://fontawesome.com/) - Ícones
- [jQuery Mask Plugin](https://igorescobar.github.io/jQuery-Mask-Plugin/) - Máscaras de input

### **Arquitetura e Padrões**
- ✅ **Clean Architecture** (4 camadas)
- ✅ **Domain-Driven Design (DDD)**
- ✅ **SOLID Principles**
- ✅ **Repository Pattern**
- ✅ **Unit of Work Pattern**
- ✅ **DTO Pattern**
- ✅ **Dependency Injection**
- ✅ **Separation of Concerns**

---

## 🏗️ Arquitetura

O projeto segue **Clean Architecture** com 4 camadas bem definidas:

```
┌─────────────────────────────────────────────┐
│              Organizzae.Web                 │  ← Camada de Apresentação
│         (Controllers, Views, MVC)           │     (ASP.NET Core MVC)
└────────────────┬────────────────────────────┘
                 │ depende de
        ┌────────┴─────────┐
        ▼                  ▼
┌──────────────────┐ ┌─────────────────────┐
│   Application    │ │   Infrastructure    │    ← Camada de Aplicação
│  (Services,DTOs) │ │  (EF Core,Repos)    │       e Infraestrutura
└────────┬─────────┘ └──────────┬──────────┘
         │ depende de           │ depende de
         └──────────┬───────────┘
                    ▼
         ┌─────────────────┐
         │     Domain      │                   ← Camada de Domínio
         │  (Entities)     │                      (Pura, sem dependências)
         └─────────────────┘
```

### **Estrutura de Pastas**

```
Organizzae/
├── src/
│   ├── Organizzae.Domain/              # Camada de Domínio
│   │   ├── Entities/                   # Entidades (Usuario, Despesa, Receita, etc)
│   │   ├── Enums/                      # Enumeradores (Status, TipoRecorrencia)
│   │   └── Interfaces/                 # Contratos de repositórios
│   │
│   ├── Organizzae.Application/         # Camada de Aplicação
│   │   ├── DTOs/                       # Data Transfer Objects
│   │   ├── Services/                   # Serviços de negócio
│   │   ├── Mappings/                   # Perfis do AutoMapper
│   │   ├── Validators/                 # Validadores FluentValidation
│   │   └── Interfaces/                 # Contratos de serviços
│   │
│   ├── Organizzae.Infrastructure/      # Camada de Infraestrutura
│   │   ├── Data/
│   │   │   ├── Context/                # DbContext do EF Core
│   │   │   └── Configurations/         # Configurações de entidades
│   │   └── Repositories/               # Implementação de repositórios
│   │
│   └── Organizzae.Web/                 # Camada de Apresentação
│       ├── Controllers/                # Controllers MVC
│       ├── Views/                      # Razor Views
│       ├── wwwroot/                    # Arquivos estáticos (CSS, JS)
│       └── Program.cs                  # Configuração da aplicação
│
├── Organizzae.sln                      # Solução Visual Studio
├── README.md                           # Este arquivo
└── LICENSE                             # Licença MIT
```

---

## 📦 Pré-requisitos

Antes de começar, você precisa ter instalado:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server 2019+](https://www.microsoft.com/sql-server/sql-server-downloads) ou SQL Server Express
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

---

## 🚀 Instalação

### **1. Clone o repositório**

```bash
git clone https://github.com/seu-usuario/organizzae.git
cd organizzae
```

### **2. Restaure os pacotes NuGet**

**Opção A - Script Automático (Recomendado):**
```bash
# Windows
.\install-packages.ps1

# Linux/macOS
chmod +x install-packages.sh
./install-packages.sh
```

**Opção B - Manualmente:**
```bash
dotnet restore Organizzae.sln
```

### **3. Configure a Connection String**

Edite o arquivo `src/Organizzae.Web/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=OrganizzaeDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**Alternativas:**
- SQL Express: `Server=localhost\\SQLEXPRESS;Database=OrganizzaeDB;...`
- LocalDB: `Server=(localdb)\\MSSQLLocalDB;Database=OrganizzaeDB;...`
- SQL Auth: `Server=localhost;Database=OrganizzaeDB;User Id=sa;Password=SuaSenha;...`

### **4. Crie o banco de dados**

```bash
# Instalar ferramenta EF (se necessário)
dotnet tool install --global dotnet-ef

# Criar migration
dotnet ef migrations add InitialCreate \
  --project src/Organizzae.Infrastructure \
  --startup-project src/Organizzae.Web

# Aplicar ao banco
dotnet ef database update \
  --project src/Organizzae.Infrastructure \
  --startup-project src/Organizzae.Web
```

### **5. Execute a aplicação**

```bash
cd src/Organizzae.Web
dotnet run
```

Acesse: **https://localhost:7107** ou **http://localhost:5208**

---

## 💻 Como Usar

### **1. Criar uma Conta**

1. Acesse a aplicação
2. Clique em **"Cadastre-se"**
3. Preencha:
   - Nome completo
   - CPF (será validado)
   - E-mail
   - Senha (mínimo 6 caracteres)
4. Clique em **"Cadastrar"**

### **2. Fazer Login**

1. Digite seu **CPF** e **Senha**
2. Marque **"Lembrar-me"** para sessão de 30 dias (opcional)
3. Clique em **"Entrar"**

### **3. Dashboard**

Após o login, você verá:
- **Métricas do mês:** Total de receitas, despesas e saldo
- **Gráficos:** Distribuição por categoria
- **Alertas:** Despesas pendentes e atrasadas
- **Top 5:** Maiores gastos do mês
- **Próximos vencimentos:** Próximos 7 dias

### **4. Cadastrar Despesa**

1. Menu **"Despesas"** → **"Nova Despesa"**
2. Preencha:
   - Descrição (ex: "Conta de Luz")
   - Valor (ex: R$ 150,00)
   - Data de vencimento
   - Categoria (Moradia, Alimentação, etc)
   - Recorrência (opcional)
3. Clique em **"Salvar"**

### **5. Registrar Pagamento**

1. Na lista de despesas, clique em **"Registrar Pagamento"**
2. Informe:
   - Data do pagamento
   - Forma de pagamento (PIX, Cartão, etc)
3. Clique em **"Confirmar"**

---

## 📊 Estrutura do Banco de Dados

### **Tabelas Principais**

| Tabela | Descrição | Registros Iniciais |
|--------|-----------|-------------------|
| **Usuarios** | Usuários do sistema | 0 |
| **Categorias** | Categorias pré-definidas | 14 (9 despesas + 5 receitas) |
| **Despesas** | Despesas cadastradas | 0 |
| **Receitas** | Receitas cadastradas | 0 |
| **Objetivos** | Objetivos financeiros | 0 |

### **Categorias Padrão**

**Despesas:**
🏠 Moradia • 🍽️ Alimentação • 🚗 Transporte • ❤️ Saúde • 🎓 Educação • 🎮 Lazer • 👕 Vestuário • 💡 Contas • ➕ Outros

**Receitas:**
💰 Salário • 💻 Freelance • 📈 Investimentos • 🛒 Vendas • ➕ Outros

---

## 📸 Capturas de Tela

### **Tela de Login**
- Design clean com gradiente roxo/azul
- Máscara automática para CPF
- Toggle de visibilidade de senha
- Link para cadastro

### **Dashboard**
- Cards de métricas (receitas, despesas, saldo)
- Gráficos de pizza interativos (Chart.js)
- Alertas coloridos (pendentes, atrasadas)
- Tabela de próximos vencimentos

### **Gestão de Despesas**
- Listagem responsiva com filtros
- Badges de status coloridos
- Modal de registro de pagamento
- Confirmação de exclusão

---

## 🗺️ Roadmap

### **✅ Versão 1.0 (Concluído)**
- [x] Autenticação com CPF e senha
- [x] CRUD de despesas
- [x] Dashboard com gráficos
- [x] Registro de pagamentos
- [x] Categorias pré-definidas
- [x] Interface responsiva

### **🚧 Versão 1.1 (Em Desenvolvimento)**
- [ ] CRUD de receitas
- [ ] CRUD de objetivos financeiros
- [ ] Relatórios em PDF
- [ ] Perfil do usuário (edição)
- [ ] Categorias personalizadas

### **📋 Versão 2.0 (Planejado)**
- [ ] Exportação para Excel
- [ ] Gráficos de evolução mensal
- [ ] Multi-moeda
- [ ] Notificações por e-mail
- [ ] API REST
- [ ] Aplicativo mobile (Xamarin/MAUI)

### **💡 Ideias Futuras**
- [ ] Integração com Open Banking
- [ ] Análise com IA/ML
- [ ] Modo escuro
- [ ] Comparativo com outros usuários
- [ ] Gamificação (badges, conquistas)

---

## 🧪 Testes

### **Executar Testes Unitários**

```bash
# Quando implementados
dotnet test
```

### **Cobertura de Código**

```bash
# Quando implementado
dotnet test /p:CollectCoverage=true
```

---

## 🤝 Contribuindo

Contribuições são sempre bem-vindas! Siga os passos:

### **1. Fork o projeto**

Clique em "Fork" no GitHub

### **2. Crie uma branch**

```bash
git checkout -b feature/MinhaNovaFeature
```

### **3. Commit suas mudanças**

```bash
git commit -m 'Add: Nova feature incrível'
```

### **4. Push para a branch**

```bash
git push origin feature/MinhaNovaFeature
```

### **5. Abra um Pull Request**

Descreva suas mudanças e aguarde a revisão!

### **Convenções de Commit**

Utilize [Conventional Commits](https://www.conventionalcommits.org/):

- `feat:` Nova funcionalidade
- `fix:` Correção de bug
- `docs:` Documentação
- `style:` Formatação
- `refactor:` Refatoração
- `test:` Testes
- `chore:` Tarefas de manutenção

---

## 📝 Licença

Este projeto está sob a licença **MIT**. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

## 👨‍💻 Autor

**Bruno** - Desenvolvedor .NET

- 📍 Araxá, Minas Gerais, Brasil
- 💼 [LinkedIn](https://www.linkedin.com/in/brunogoncalveslemos)
- 📧 [E-mail](mailto:bruno.goncalves1999@hotmail.com)
- 🌐 [Portfolio](https://devbrunogoncalves.vercel.app)

### **Sobre Mim**

Desenvolvedor .NET com 4 anos de experiência, especializado em **Clean Architecture**, **DDD** e desenvolvimento de aplicações web modernas. Apaixonado por código limpo, boas práticas e arquitetura de software.

**Competências Técnicas:**
- ASP.NET Core MVC/Web API
- Entity Framework Core
- SQL Server / PostgreSQL
- Clean Architecture & DDD
- SOLID & Design Patterns
- Git & GitHub

---

## 🙏 Agradecimentos

- **LocalizaLabs** - Bootcamp .NET Developer
- **ASP.NET Core Expert** - Formação avançada
- **Comunidade .NET** - Suporte e aprendizado contínuo
- **LuisDev** - Mentoria .Net Expert

---

## 📚 Documentação Adicional

- [📖 Guia de Instalação Completo](INSTALL.md)
- [📦 Pacotes e Referências](PACKAGES.md)
- [🚀 Início Rápido](QUICKSTART.md)
- [🔐 Fluxo de Autenticação](AUTHENTICATION-FLOW.md)
- [🔧 Troubleshooting](TROUBLESHOOTING.md)

---

## 📊 Estatísticas do Projeto

- **Linhas de código:** ~8.000+
- **Arquivos:** 89
- **Commits:** [seu número de commits]
- **Tecnologias:** 15+
- **Padrões de design:** 7+

---

## 🌟 Destaque do Projeto

Este projeto demonstra:

✅ **Arquitetura Profissional** - Clean Architecture com separação clara de responsabilidades  
✅ **Código Limpo** - Seguindo SOLID e boas práticas do C#  
✅ **Segurança** - Autenticação, autorização, hash de senhas, CSRF protection  
✅ **UX Moderna** - Interface responsiva e intuitiva com Bootstrap 5  
✅ **Testes de Qualidade** - Validações robustas com FluentValidation  
✅ **Documentação Completa** - README detalhado e guias de uso  

**Ideal para portfólio profissional e demonstração de habilidades em .NET!**

---

<div align="center">

**Desenvolvido com ❤️ e ☕ por Bruno**

⭐ **Se este projeto foi útil, deixe uma estrela!** ⭐

[⬆ Voltar ao topo](#-organizzae---sistema-de-gestão-financeira-pessoal)

</div>
