# EuPagoAPI
Web Api do projeto EuPago.

# Objetivo do projeto
O EuPago é muito mais que uma “carteira digital”, como o “Apple Pay” já 
disponível no mercado, dentre outros aplicativos similares, e tão pouco será
apenas um leitor de tela que permita ao deficiente saber o valor a ser cobrado de 
sua conta (o que também abre margens para falhas e fraudes).
Nossa proposta, é disponibilizar uma aplicação que fara uso da tecnologia NFC 
(Near Field Communication), que permitirá obter os dados já registrados na 
maquina do cartão que estão aguardando pagamento, como por ex: 
Estabelecimento, Valor da Compra, Parcelamento, Juros de Parcelamento e Valor 
Total com Juros. Uma vez de posse dessas informações, nosso aplicativo fará a 
“tradução” dos dados recebidos para que seja informado via Assistente Virtual de 
Voz, para que o Deficiente Visual possa saber exatamente quais os dados de 
cobrança que esta prestes a quitar com seu cartão.

# Arquitetura da solução
<img width="616" alt="image" src="https://user-images.githubusercontent.com/27664062/190928198-34dbbc23-4e2e-4ea6-8218-2999f254992e.png">

# Tabela de endpoints

## Usuario
![image](https://user-images.githubusercontent.com/27664062/190928251-86ae01df-212f-48b1-b406-e48d74fb240b.png)

### POST /api/usuario/login

### Descrição

Endpoint utilizado para realizar o login do usuário.

Corpo da requisição
```json
{
  "cpf": 0,
  "senha": "string"
}
```

Resposta - Status Code 200
```json
{
  "usuario": {
    "nome": "string",
    "cpf": 0,
    "senha": "string",
    "dataNascimento": "2022-09-18T21:10:42.988Z",
    "statusVisao": "string",
    "email": "string",
    "statusCadastro": "string",
    "dataCadastro": "2022-09-18T21:10:42.988Z",
    "dataAtualizacao": "2022-09-18T21:10:42.988Z",
    "telefone": {
      "ddd": 0,
      "numero": 0
    },
    "endereco": {
      "cidade": "string",
      "rua": "string",
      "bairro": "string",
      "complemento": "string",
      "cep": "string",
      "numero": 0
    }
  },
  "bearer": {
    "token": "string"
  }
}
```

### POST /api/usuario

### Descrição

Endpoint utilizado para registrar um novo usuário.

Corpo da requisição
```json
{
  "usuario": {
    "nome": "string",
    "cpf": 0,
    "senha": "string",
    "dataNascimento": "2022-09-18T21:14:22.468Z",
    "statusVisao": "string",
    "email": "string",
    "statusCadastro": "string",
    "dataCadastro": "2022-09-18T21:14:22.468Z",
    "dataAtualizacao": "2022-09-18T21:14:22.468Z",
    "telefone": {
      "ddd": 0,
      "numero": 0
    },
    "endereco": {
      "cidade": "string",
      "rua": "string",
      "bairro": "string",
      "complemento": "string",
      "cep": "string",
      "numero": 0
    }
  }
}
```

Resposta - Status Code 200
```json
{
  "usuario": {
    "nome": "string",
    "cpf": 0,
    "senha": "string",
    "dataNascimento": "2022-09-18T21:14:22.471Z",
    "statusVisao": "string",
    "email": "string",
    "statusCadastro": "string",
    "dataCadastro": "2022-09-18T21:14:22.471Z",
    "dataAtualizacao": "2022-09-18T21:14:22.471Z",
    "telefone": {
      "ddd": 0,
      "numero": 0
    },
    "endereco": {
      "cidade": "string",
      "rua": "string",
      "bairro": "string",
      "complemento": "string",
      "cep": "string",
      "numero": 0
    }
  }
}
```

### PUT /api/usuario

### Descrição

Endpoint utilizado para editar um usuário.

Corpo da requisição
```json
{
  "usuario": {
    "nome": "string",
    "cpf": 0,
    "senha": "string",
    "dataNascimento": "2022-09-18T21:16:10.523Z",
    "statusVisao": "string",
    "email": "string",
    "statusCadastro": "string",
    "dataCadastro": "2022-09-18T21:16:10.523Z",
    "dataAtualizacao": "2022-09-18T21:16:10.523Z",
    "telefone": {
      "ddd": 0,
      "numero": 0
    },
    "endereco": {
      "cidade": "string",
      "rua": "string",
      "bairro": "string",
      "complemento": "string",
      "cep": "string",
      "numero": 0
    }
  }
}
```
Resposta - Status Code 200

```json
{
  "usuario": {
    "nome": "string",
    "cpf": 0,
    "senha": "string",
    "dataNascimento": "2022-09-18T21:16:10.526Z",
    "statusVisao": "string",
    "email": "string",
    "statusCadastro": "string",
    "dataCadastro": "2022-09-18T21:16:10.526Z",
    "dataAtualizacao": "2022-09-18T21:16:10.526Z",
    "telefone": {
      "ddd": 0,
      "numero": 0
    },
    "endereco": {
      "cidade": "string",
      "rua": "string",
      "bairro": "string",
      "complemento": "string",
      "cep": "string",
      "numero": 0
    }
  },
  "bearer": {
    "token": "string"
  }
}
```

### DELETE /api/usuario/{cpf}

### Descrição

Endpoint utilizado para deletar(soft delete)/desativar um usuário.

Parâmetros

CPF - integer(int64)

Resposta - Status Code 200

```json
{
  "id": 0,
  "nome": "string",
  "telefone": {
    "id": 0,
    "ddd": 99,
    "numero": 999999999,
    "usuarioId": 0
  },
  "endereco": {
    "id": 0,
    "cidade": "string",
    "rua": "string",
    "bairro": "string",
    "numero": 0,
    "complemento": "string",
    "cep": "string",
    "usuarioId": 0
  },
  "cpf": 0,
  "senha": "stringstri",
  "dataNascimento": "2022-09-18T21:23:27.380Z",
  "statusVisao": "string",
  "email": "user@example.com",
  "statusCadastro": "string",
  "dataCadastro": "2022-09-18T21:23:27.380Z",
  "dataAtualizacao": "2022-09-18T21:23:27.380Z"
}
```

## Como executar o projeto

Faça o download do repositório na sua máquina, abra a solution 'EuPagoAPI' no Visual Studio e execute-a. A API foi documentada com SwaggerAPI, que fica disponivel na url https://localhost:7059/swagger/index.html.

## Usuários já cadastrados para teste

* CPF: 12345678907 - Senha: teste123455
* CPF: 12345678900 - Senha: xpto123teste

## Integrantes

* André Hugo Bastos da Silva - 88588
* Helouíse Cristina de Almeida Itokazo - 85110
* Marcus Vinnicius Carvalho dos Santos - 88469
* Matheus Ferreira Santana - 88241
* Oswaldo Gomes Moreira - 88526
