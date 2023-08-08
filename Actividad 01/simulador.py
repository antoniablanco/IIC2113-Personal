import random


L = [1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1]
M = [[0, 1, [], True], [16, 1, [], False], [32, 1, [], False], [16, -1, [], False], [32, -1, [], False], [47, -1, [], True]]
E = {i: [] for i in range(len(L)) if L[i] == 1}
T = []
for t in range(1, 601):
	for e in E:
		for _ in range(random.randint(0,10)):
			d = random.choice(list(E.keys()))
			while d == e:
				d = random.choice(list(E.keys()))
			E[e].append((d,t))
	for m in M:
		if m[-1]:
			for i in range(len(m[2])-1, -1, -1):
				if m[2][i][0] == m[0]:
					T.append(t-m[2][i][1])
					m[2].pop(i)
			i = 0
			while i < len(E[m[0]]):
				if ((E[m[0]][i][0] - m[0]) * m[1]) > 0:
					m[2].append(E[m[0]][i])
					E[m[0]].pop(i)
				else:
					i += 1
			m[-1] = False
		else:
			m[0] += m[1]
			if m[0] in [0, len(L)-1]:
				m[1] *= -1
			if L[m[0]] == 1:
				m[-1] = True

	print("\n----------------------------")
	print(f"Tiempo: {t}")
	print("\nMetros:")
	for m in M:
		d = 'derecha' if m[1] == 1 else 'izquierda'
		print(f"\tMetro en posición {m[0]} avanza hacia {d} con {len(m[2])} pasajeros")
	print("\nEstaciones:")
	for e in E:
		print(f"\tEstación en posición {e} tiene {len(E[e])} esperando en el andén")
	input()

print(f"\nEl tiempo medio de viaje fue {sum(T)/len(T):0.1f} minutos")