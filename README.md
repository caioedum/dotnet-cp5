# 🚀 Sistema de Mensageria com RabbitMQ e .NET

Projeto de exemplo para envio e processamento de mensagens usando RabbitMQ, com validação centralizada e roteamento inteligente.

![RabbitMQ + .NET](https://img.shields.io/badge/RabbitMQ-.NET%208-FF6F00?logo=rabbitmq&logoColor=white)

## 📋 Visão Geral
Sistema composto por:
- **Senders**: Aplicações para envio de mensagens
- **Validation Service**: Serviço de validação central
- **Receivers**: Consumidores de mensagens validadas

## ⚙️ Arquitetura do RabbitMQ

### 🔀 Exchanges e Routing Keys
| Exchange         | Tipo  | Descrição                     | Routing Keys Usadas         |
|------------------|-------|-------------------------------|-----------------------------|
| `topic_frutas`   | Topic | Mensagens sobre frutas        | `frutas.validacao`, `frutas.receiver1` |
| `topic_usuarios` | Topic | Mensagens sobre usuários      | `usuarios.validacao`, `usuarios.receiver2` |

### 📭 Filas Principais
| Fila                  | Exchange         | Routing Key          | Descrição                     |
|-----------------------|------------------|----------------------|-------------------------------|
| `frutas_validacao`    | `topic_frutas`   | `frutas.validacao`   | Fila de pré-validação de frutas |
| `frutas_processadas`  | `topic_frutas`   | `frutas.receiver1`   | Fila de frutas validadas      |
| `usuarios_validacao`  | `topic_usuarios` | `usuarios.validacao` | Fila de pré-validação de usuários |
| `usuarios_processados`| `topic_usuarios` | `usuarios.receiver2` | Fila de usuários validados    |

## 🛠️ Pré-requisitos
- .NET 8 SDK
- Docker e Docker Compose
- RabbitMQ 3.12+ (via Docker)
- IDE (Visual Studio 2022/VSCode)

## 🚀 Instalação
```

git clone https://github.com/caioedum/dotnet-cp5.git
cd dotnet-cp5
docker-compose up -d  \# Inicia o RabbitMQ
dotnet restore

```

## ⚡ Configuração
Edite o `docker-compose.yml` para ajustes no RabbitMQ:
```

services:
rabbitmq:
image: rabbitmq:3-management
ports:
- "5672:5672"
- "15672:15672"
environment:
RABBITMQ_DEFAULT_USER: guest
RABBITMQ_DEFAULT_PASS: guest

```

## ▶️ Execução
Execute em terminais separados:

1. **Serviço de Validação**
```

dotnet run --project ValidationService

```

2. **Receivers**
```

dotnet run --project ReceiveMessages 1  \# Receiver de Frutas
dotnet run --project ReceiveMessages 2  \# Receiver de Usuários

```

3. **Senders**
```

dotnet run --project SendMessages

```
👉 No menu interativo:
- `1` para enviar frutas
- `2` para enviar usuários
- `X` para sair

## 🧪 Testes
Acesse o **RabbitMQ Management**:
```

http://localhost:15672

```
Credenciais: `guest`/`guest`

Verifique:
- ✅ Conexões ativas em `Connections`
- 🗃️ Mensagens nas filas em `Queues`
- 📊 Estatísticas em `Exchanges`

## 🔄 Fluxo de Mensagens
### Exemplo: Fruta
1. `Sender1` → `topic_frutas` (routing key: `frutas.validacao`)
2. `ValidationService` valida e envia para `frutas.receiver1`
3. `Receiver1` processa mensagem da `frutas_processadas`

### Exemplo: Usuário
1. `Sender2` → `topic_usuarios` (routing key: `usuarios.validacao`)
2. `ValidationService` valida e envia para `usuarios.receiver2`
3. `Receiver2` processa mensagem da `usuarios_processados`

## 📦 Dependências
- .NET 8
- RabbitMQ.Client 7.x
- Newtonsoft.Json 13.x

## 📄 Licença
MIT License - Veja o arquivo [LICENSE](LICENSE) para detalhes.

---

![FIAP](https://img.shields.io/badge/Developed%20at-FIAP-8A2BE2) 
```

Este README fornece uma visão completa do sistema, incluindo a estrutura de mensageria e comandos essenciais para execução. Personalize com seus dados reais de repositório quando for utilizar! 🐇💻

