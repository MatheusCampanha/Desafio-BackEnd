Aplicação de Gerenciamento de Pedidos e Locação de Motos
Esta aplicação foi desenvolvida para o gerenciamento de pedidos e locação de motos, utilizando as seguintes tecnologias:

.NET 8: Utilizado para desenvolvimento tanto da WebAPI quanto da WebAPP.
MongoDB: Utilizado para armazenamento de dados.
AmazonMQ: Utilizado para gerenciamento da fila de mensagens.
Amazon S3: Utilizado para armazenar a imagem da CNH do entregador.
Branches
O código finalizado está disponível na branch master, enquanto todos os commits estão acessíveis na branch dev.

Usuários Padrão
Para fins de demonstração, dois usuários padrão já estão cadastrados:

Usuário: admin, Senha: admin
Usuário: entregador, Senha: entregador
No entanto, os usuários têm a opção de criar contas com diferentes perfis. A aplicação está configurada para permitir a locação de moto somente após o cadastro do entregador. Portanto, após criar um novo usuário, é necessário cadastrá-lo como entregador.
