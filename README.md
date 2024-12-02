# Visão Geral do Jogo

- **Título: The Capo´s Rise**

- **Gênero: Ação/Plataforma 2D**

- **Plataforma: PC (Windows/Mac)**

**Descrição:** The Capo´s Rise é um jogo de plataforma 2D onde o jogador controla um mafioso acabar com seguranças de uma indústria derrotando os seguranças do local. O jogo está dividido em três fases distintas: um Tutorial para entender o conceito do jogo, o Nível 1 com dificuldade normal e o Nível 2 com dificuldade difícil. Sendo o mais difícil contendo inimigos mais resistentes e um vida escassa.
Objetivo: O objetivo principal é completar cada nível superando os inimigos e obstáculos, coletando corações e alcançando o final de cada fase. No Nível 2, os desafios são mais intensos, exigindo maior habilidade e estratégia por parte do jogador. Ao final de cada fase será mostrado a quantidade de inimigos mortos e quantas vidas restaram do player.

# Controles

- **Mover para a esquerda: A**
- **Mover direita: D**
- **Pular: Space**
- **Atirar: C**
- **Pausar Jogo: ESC**

# Níveis
## 1. **Tutorial**
- **Objetivo:** Introduzir os jogadores aos controles básicos e mecânicas do jogo.

- **Características:**
**Ambiente Controlado:** Poucos obstáculos e inimigos.
**Inimigos Simples:** Inimigos básicos com pouca saúde para prática de combate.

## 2. **Nível 1 - Normal**
- **Objetivo:** Fornecer um desafio equilibrado que reforça as habilidades aprendidas no Tutorial.

- **Características:**
**Variedade de Inimigos:** Introdução de diferentes tipos de inimigos
**Desafios de Plataforma:** Plataformas e obstáculos simples.
**Power-ups:** Corações para que o jogador possa ter mais resistência contra os inimigos.

**Dificuldade:** Normal – adequado para jogadores que dominaram os controles e mecânicas básicas.

**Elementos de Design:**
- **Layout Balanceado:** Combinação de seções de combate e plataforma.


## 3. **Nível 2 - Difícil**
- **Objetivo:** Desafiar jogadores com inimigos mais difíceis.

- **Características:**
**Tipos de Inimigos Avançados:** Inimigos com maior saúde, movimento mais rápido.
**Recursos Limitados:** Limitada quantia de corações encontradas pela fase.
**Batalha de Chefe:** Um chefe desafiador no final do nível.

**Dificuldade:** Difícil – projetado para jogadores que buscam um desafio significativo.

**Elementos de Design:**

- **Alta Densidade de Inimigos:** Encontros frequentes para testar as habilidades de combate do jogador.

# Inimigos e Itens

## Tipos de inimigos

### 1.Inimigo Básico
- **Comportamento:** Move-se em direção ao jogador lentamente e atira nele
- **Saúde:** Baixa.

### 2. Inimigo Rápido
- **Comportamento:** Move-se mediano em direção ao jogador e atira nele
- **Saúde:** Média.

### 3. Inimigo Atirador
- **Comportamento:** Move-se rapidamente em direção ao jogador e atira nele
- **Saúde:** Alta.

### 4. Chefe
- **Comportamento:** Move-se rapidamente em direção ao jogador e atira nele
- **Saúde:** Alta x2.


## Itens

### 1. Corações
- **Objetivo do item:** Aumentar a quantia total de corações ao qual ele inicia a fase.

# Audio

## Efeitos Sonoros

### Ações do Jogador:

- **Pular:** Som de salto para feedback ao jogador.
- **Atirar:** Som de disparo de arma ou tiro.
- **Receber Dano:** Som que indica que o jogador foi atingido.
- **Morte:** Som ao perder todas as vidas ou saúde.

### Ações dos inimigos:

- **Atacar:** Sons de tiro dar armas dos inimigos.
- **Morrer:** Sons que indicam a morte do inimigo.
- **Andar:** Sons que indicam o andar do inimigo.
- **Receber Dano:** Sons que indicam que o inimigo recebeu dano.

## Músicas
- **Cada nível possui sua trilha sonora com copyright nelas.**

# Implementação no Unity

## 1. Fontes de Áudio (Audio Sources).
## 2. Mixer de Áudio (Audio Mixer).
## 3. Triggers de Áudio.
## 4. Controle da câmera no personagem (Cinemachine).
## 5. Controle do personagem e dos inimigos com o cenário (RigidBody 2d)
## 6. Colisor do persoangem e dos inimigos (Capsule Collider 2d)

# Vídeo de Demo
- **Anexado a este projeto contém um vídeo explicando uma pequena demo do jogo explicado por mim mesmo (Bernardo Zaparoli) as funcionalidades do jogo, itens e inimigos presentes no jogo**

Fim da Documentação
______________________________________________________________________________________________________