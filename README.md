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

Corpo da requisição
```json
{
  "cpf": 0,
  "senha": "string"
}
```

Resposta
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
