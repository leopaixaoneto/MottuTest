<p align="center">
  <a href="https://mottu.com.br/">
    <picture>
      <img src="https://cdn.discordapp.com/attachments/855339357157064765/1193238479186964610/Mottu-grupo-verde-horizontal.png" height="128">
    </picture>
    <h1 align="center">Mottu Url Shortener</h1>
  </a>
</p>

<p align="center">
  <a aria-label="Mottu" href="https://mottu.com.br/">
    <img src="https://img.shields.io/badge/Feito%20Para-Mottu-green.svg?style=for-the-badge">
  </a>
  <img alt="" src="https://img.shields.io/badge/versão-.Net7-blue.svg?style=for-the-badge">
  <a aria-label="Linkedin" href="https://www.linkedin.com/in/leopaixaoneto/">
    <img alt="" src="https://img.shields.io/badge/Linkedin-blue.svg?style=for-the-badge&logo=Linkedin&labelColor=0A66C2&logoWidth=20">
  </a>
</p>

## Contextualização
Este projeto é um projeto do estilo comprovação de conhecimento.

Visite <a aria-label="Aprenda sobre a Mottu" href="https://mottu.com.br">Mottu</a> para conhecer a empresa e seu segmento.

O objetivo deste, da-se pela construção de um encurtador de URL no estilo <a aria-label="Aprenda sobre TinyURL" href="https://tinyurl.com/app">TinyURL</a>


## Documentação

Visite o <a aria-label="Arquivo descritivo" href="https://mottucombr-my.sharepoint.com/:w:/g/personal/andre_porto_mottu_com_br/EfaytSfKUnRbMGCVWlW8z5wBj3tQJnwYcu0cFrpXgSvGiQ?e=4%3Ay7V2vk&at=9&CID=F21BFA81-3F8D-47FD-B252-14320F974474&wdLOR=cFB20A7DB-AEDE-4E68-8C6A-764D6F56D20D">Arquivo</a> descritivo do problema proposto.

## Explicação da solução

Para solucionar o problema abordado, optei por dividir a solução em alguns projetos separados para isolar as responsabilidades da aplicação e aumentar a performance como um todo, visando uma grande quantidade de usuários simultâneos.

<br>
<p align="center">  
  Arquitetura escolhida para resolver o problema
</p>
<p align="center">
  <img src="https://i.imgur.com/MpND112_d.webp?maxwidth=760&fidelity=grand" height="512" />
</p>

- <b>Api central:</b> 
  - Responsável pelo gerenciamento das URL's encurtadas com funções de criação e consumo (redirecionar usuario).
  - Possuí um banco de dados in-memory (redis) que serve como caching de requisições afim de aumentar a performance, diminuir a latência e diminuir consumo do banco de dados
  - Responsável por disparar os eventos de criação e visualização de url's
  - Comunica-se com as api's de suporte através de um Message Broker (rabbitMQ)
  * OBS: A Api central foi idealizada de forma a ser executada em formato de cluster, por isso foi escolhido ter cache com redis, para centralizar o cache.
  * OBS2: O LoadBalancing existe no projeto e está pré-configurado para funcionar, porém não faz parte do docker-compose default

 
 - <b>Api KGS - Suporte:</b>
   - Responsável pela geração e disponibilização de chaves encurtadas (identificadores)
   - Responsável por gerenciar as chaves existentes usadas e as disponíveis
   - Possuí um banco de dados postgresql para armazenar as chaves criadas
   - Possuí um cache in-memory afim de ter chaves disponíveis em memória sempre que a Api central precisar
   - Possuí uma rotina de verificação de quantidade de chaves disponiveis em memória (preenchendo a memória caso necessário)
   - Possuí uma rotina de verificação de quantidade de chaves disponíveis no banco de dados (fazendo a geração de novas caso necessário)
   - Comunica-se com a Api central através de um Message Broker (rabbitMQ)

 
- <b>Api Analytics - Suporte:</b>
  - Responsável pelo processamento de eventos das Urls encurtadas (criação e visualização)
  - Responsavel pela geração de "relatório" que contabilizam os eventos para cada uma das chaves armazenadas
  - Possuí um banco de dados postgresql para armazenar os eventos processados, recebidos.
  - Possuí um cache in-memory afim de aumentar a velocidade de resposta para requisições repetidas da Api central
  - Comunica-se com a Api central através de um Message Broker (rabbitMQ)


## Explicação dos projetos
<p>
  Dentro da solução existem alguns projetos, sendo seus nomes e respectivas funções:
</p>

  | Projeto            | Descrição                           |
  | ------------------ | --------------------------------------------------------- |
  | `MottuApi`     | Api principal, responsável por gerar e redirecionar as url encurtadas |
  | `MottuKGS`      | Api de suporte, responsável por pré-gerar chaves de identificação de url encurtada, para a api principal|
  | `MottuAnalytics` | Api de suporte, responsável por eventos relacionados a Api principal, como created e viewed de uma url encurtada |
  | `MottuShared`       | Projeto de suporte, responsável por centralizar classes, funções e configurações usadas por mais de um projeto |
  | `MottuLB` | Projeto de suporte, pré configurado para servir de LoadBalancer para a Api principal|


## Iniciando o projeto
<p>
  Parto do pressuposto de que os avaliadores terão em suas respectivas máquinas as versões de C# e Docker necessárias para a execução do projeto.
  <br>
</p>
<p>
  Após fazer o cloning deste projeto a partir do comando:
</p>

```Shell
git clone https://github.com/leopaixaoneto/MottuTest.git
```

<p>
  Abra a pasta clonada destino.<br><br>
  Você encontrará dentro dela uma pasta denominada "_extras".<br><br>
  Dentro dela você encontrará:<br>
</p>

1. Bash para inicialização do projeto como um todo
2. Bash para inicialização dos bancos de dados do projeto
3. Coleção de requisições insomnia para testes
4. O desenho da arquitetura

Para iniciar o projeto:
1. abra o bash: "docker_start.bat"
2. Espere a inicialização completa
   1. Caso seja a primeira vez, você deve abrir o bash "database_update_batch.bat" em seguida para aplicar as migrations aos bancos de dados.

Após iniciados, verifique se todos os serviços estão ligados no container Docker.

Faça os testes a partir da collection insomnia contida na mesma pasta.

Recomendo que execute as chamadas varias vezes seguidas para observar a diminuição do tempo de execução, ganhos adquiridos pelas técnicas utilizadas no projeto (caching, comunication, etc)


## Tecnologias Usadas
<p float="left">
  <img alt="" src="https://img.shields.io/badge/CSharp-239120?style=for-the-badge&logo=csharp&logoColor=white">
  <img alt="" src="https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white">
  <img alt="" src="https://img.shields.io/badge/redis-%23DD0031.svg?&style=for-the-badge&logo=redis&logoColor=white">
  <img alt="" src="https://img.shields.io/badge/rabbitmq-%23FF6600.svg?&style=for-the-badge&logo=rabbitmq&logoColor=white">
</p>

## Autor
- Leonardo Paixão Viana ([@leopaixaoneto](https://www.linkedin.com/in/leopaixaoneto/))
