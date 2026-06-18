"""
Gerador de gráfico de fitness por geração — Flappy Bird IA
==========================================================
Como usar:
  1. Rode o projeto no Unity por pelo menos 30 gerações
  2. A cada geração, anote o valor de "Melhor Fitness" exibido no HUD
  3. Preencha a lista `dados` abaixo com os valores coletados
  4. Execute: python3 gerar_grafico_fitness.py
  5. O gráfico será salvo como "grafico_fitness.png" nesta pasta
"""

import sys

# ─────────────────────────────────────────────────────────
# PREENCHA AQUI com os valores do HUD (um por geração)
# Exemplo: [0.3, 1.2, 2.8, 4.1, 5.5, ...]
dados = [
    # geração 1, 2, 3, ...
]
# ─────────────────────────────────────────────────────────

if len(dados) == 0:
    print("ATENÇÃO: a lista 'dados' está vazia.")
    print("Preencha com os valores de Melhor Fitness coletados no HUD do Unity.")
    sys.exit(1)

try:
    import matplotlib.pyplot as plt
    import matplotlib.ticker as ticker
except ImportError:
    print("Instalando matplotlib...")
    import subprocess
    subprocess.check_call([sys.executable, "-m", "pip", "install", "matplotlib"])
    import matplotlib.pyplot as plt
    import matplotlib.ticker as ticker

geracoes = list(range(1, len(dados) + 1))

fig, ax = plt.subplots(figsize=(12, 6))
fig.patch.set_facecolor("#0D0D0D")
ax.set_facecolor("#1A1A2E")

ax.plot(geracoes, dados,
        color="#00C8FF", linewidth=2.5, marker="o",
        markersize=5, markerfacecolor="#FFD700", markeredgewidth=0,
        label="Melhor fitness")

ax.fill_between(geracoes, dados, alpha=0.15, color="#00C8FF")

melhor_gen = geracoes[dados.index(max(dados))]
melhor_val = max(dados)
ax.annotate(f"  Máx: {melhor_val:.1f}s\n  Geração {melhor_gen}",
            xy=(melhor_gen, melhor_val),
            xytext=(melhor_gen + max(1, len(dados) * 0.05), melhor_val * 0.95),
            color="#FFD700", fontsize=11,
            arrowprops=dict(arrowstyle="->", color="#FFD700", lw=1.5))

ax.set_title("Evolução do Agente por Geração",
             color="#FFD700", fontsize=16, fontweight="bold", pad=15)
ax.set_xlabel("Geração", color="#E0E0E0", fontsize=13)
ax.set_ylabel("Melhor Fitness (segundos)", color="#E0E0E0", fontsize=13)

ax.tick_params(colors="#A0A0A0", labelsize=11)
ax.xaxis.set_major_locator(ticker.MaxNLocator(integer=True))
for spine in ax.spines.values():
    spine.set_edgecolor("#2A2A40")

ax.grid(True, color="#2A2A40", linestyle="--", linewidth=0.8, alpha=0.7)
ax.legend(facecolor="#1A1A2E", edgecolor="#2A2A40",
          labelcolor="#E0E0E0", fontsize=11)

plt.tight_layout()
output = "grafico_fitness.png"
plt.savefig(output, dpi=150, facecolor=fig.get_facecolor())
print(f"Gráfico salvo em: {output}")
print(f"  Gerações registradas : {len(dados)}")
print(f"  Melhor fitness       : {max(dados):.2f}s (geração {melhor_gen})")
print(f"  Fitness inicial      : {dados[0]:.2f}s")
