# Flappy Bird com Inteligência Artificial

## Neuroevolução com Redes Neurais e Algoritmos Genéticos

> Trabalho Grau B — Inteligência Artificial | UNISINOS | 2026
> Integrantes: Rodrigo de Moraes Lehnen | Kalleu Queirolo Hannecker

---

## Descrição

Implementação do jogo **Flappy Bird** com agentes controlados por Inteligência Artificial utilizando **Neuroevolução**: uma rede neural de topologia fixa (3 → 6 → 1) cujos pesos são otimizados por um **Algoritmo Genético** com elitismo, crossover e mutação.

Cada geração simula **50 pássaros simultaneamente**. Os pássaros que sobrevivem por mais tempo passam seus "genes" (pesos da rede) para a geração seguinte. Nenhuma regra de jogo foi explicitamente programada — o comportamento emerge da pressão seletiva.

---

## Como Funciona

### A Rede Neural

Cada pássaro possui uma rede neural própria com:

| Camada  | Neurônios | Descrição                                                              |
| ------- | --------- | ---------------------------------------------------------------------- |
| Entrada | 3         | Posição Y do pássaro / Distância horizontal ao cano / Posição Y do gap |
| Oculta  | 6         | Ativação `tanh`                                                        |
| Saída   | 1         | `> 0` → bater asas · `≤ 0` → não bater                                |

### O Algoritmo Genético

```text
Inicialização → Avaliação (fitness) → Seleção (elitismo 10%)
     ↑                                         ↓
Nova Geração  ←     Mutação      ←      Crossover
```

**Fitness:** tempo de sobrevivência em segundos.

---

## Requisitos

- **Unity Editor** versão `6000.4.1f1` (Unity 6)
  → [Download via Unity Hub](https://unity.com/download)
- **Sistema operacional:** Windows 10/11 ou macOS 12+
- Não requer instalação de bibliotecas externas

---

## Como Executar

### Pelo Unity Editor

1. Instale o **Unity Hub** e adicione a versão `6000.4.1f1`
2. No Unity Hub: **Open → Add project from disk** → selecione a pasta do projeto
3. Aguarde a importação dos assets
4. No painel **Project**, abra `Assets > Scenes > SampleScene`
5. Clique em **Play (▶)** para iniciar

O HUD exibe em tempo real:

- Geração atual
- Quantidade de pássaros vivos
- Melhor fitness da geração (segundos)

### Pelo Build (Executável)

1. `File → Build Settings`
2. Selecione a plataforma (**PC** ou **WebGL** para itch.io)
3. Clique em **Build**
4. Execute o arquivo gerado

---

## Parâmetros Configuráveis

Selecione o objeto `GeneticAlgorithm` na hierarquia da cena e ajuste no **Inspector**:

| Parâmetro          | Padrão | Descrição                          |
| ------------------ | ------ | ---------------------------------- |
| `Population Size`  | 50     | Número de pássaros por geração     |
| `Mutation Rate`    | 0.05   | Probabilidade de mutação por peso  |
| `Mutation Strength`| 0.5    | Intensidade da mutação             |

---

## Estrutura do Projeto

```text
Assets/
├── Scripts/
│   ├── Bird.cs               # Agente jogador
│   ├── NeuralNetwork.cs      # Rede neural (3 → 6 → 1, tanh)
│   ├── GeneticAlgorithm.cs   # Seleção, crossover, mutação
│   ├── GameManager.cs        # Orquestra gerações e HUD
│   ├── PipeSpawner.cs        # Spawna os canos
│   └── Pipe.cs               # Movimenta e destrói cada cano
└── Scenes/
    └── SampleScene.unity     # Cena principal
```

---

## Referências

1. STANLEY, K. O.; MIIKKULAINEN, R. **Evolving Neural Networks through Augmenting Topologies.** *Evolutionary Computation*, MIT Press, v. 10, n. 2, p. 99–127, 2002.
2. TAI, M. et al. **Reinforcement Learning and Neuroevolution in Flappy Bird Game.** In: *Intelligent Systems and Applications*, Springer, 2019.
3. LIAO, C. et al. **Neuro-Evolution in Flappy Bird Game.** *IEEE Conference on Computational Intelligence*, IEEE Xplore, 2022.
4. RUSSEL, S.; NORVIG, P. **Artificial Intelligence: A Modern Approach.** 4. ed. Pearson, 2020.
5. Unity Technologies. **Unity Documentation.** Disponível em: [https://docs.unity3d.com](https://docs.unity3d.com)
