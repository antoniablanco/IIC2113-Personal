import random

L = [1, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 1]
#L = [1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1] # Son todas las posibles posiciones
# Se agregaron dos metros de la forma [24,1,[],False] y [24,-1,[],False]
M = [[0, 1, [], True, 2], [16, 1, [], False, 3], [24,1,[],False, 2], [32, 1, [], False, 3], [16, -1, [], False, 3], [24,-1,[],False, 3], [32, -1, [], False, 3], [47, -1, [], True, 2]] # Cada sublista es un metro y sigue la estructura (posicion, si va a la derecha es 1 y a la izquierda es -1, , True si debe parar en la estacion False en otro caso, 2 si es linea roja o 3 si es linea verde)
# Hay estaciones en las posiciones 0,7,14,20,27,33,38,47
# Se cambia para que cuente todas las estaciones
E = {i: [] for i in range(len(L)) if (L[i] != 0) } # Son las estaciones, calculadas al azar de todas las posiciones
T = []
# Se cambio para que sean 16 horas enves de 10
for t in range(1, 961): # Es la cantidad de tiempo en minutos
	for e in E: # Vamos viendo estacion a estación, se hace al azar todas las veces
		#print("e es:", e)
		#print("Color de la estacion es", L[e])
		# Se cambio para que sea entre 0 y 20
		for _ in range(random.randint(0,20)): #Cantidad de personas al azar que estan esperando
			d = random.choice(list(E.keys())) # Se escoge al azar una estación para que sea a la que tiene que llegar el pasajero
			recalcular = True
			while recalcular==True: # Si se esta en la misma estación se cambia
				if d != e:
					if L[d]==1 or L[d]==L[e] or L[e]==1:
						recalcular = False
					else:
						#print("se vuelve a calcular por que el color es ", L[d])
						d = random.choice(list(E.keys())) 
						recalcular = True
				else:
					#print("se vuelve a calcular por que el color es ", L[d])
					d = random.choice(list(E.keys())) 
					recalcular = True
			#print("Estacion final es:", d, "y su color:", L[d])
			E[e].append((d,t)) # Se agrega la persona a la lista de la estación con el tiempo en 
	for m in M: # Vamos viendo metro a metro
		#print(f"Tren en la posicion {m[0]} es de color {m[4]}")
		# Se cambia por que ahora el true y false no esta en la ultima posición
		if m[-2]: # Primero se ve si es True o False, que implica si debe parar en la estación pasajeros o no 
			for i in range(len(m[2])-1, -1, -1): # Se va viendo cada pasajero del metro para bajarse
				if m[2][i][0] == m[0]: # Si el lugar de destino del pasajero es donde se encuentra
					T.append(t-m[2][i][1]) # Se agrega el tiempo que se demoro en llegar
					m[2].pop(i) # Se elimina de la lista al pasajero
			i = 0 # Se reinicia el contador
			while i < len(E[m[0]]): # Se va viendo cada pasajero de la estación para subirse
				if len(m[2])<250: # Se agrego una limitación de pasajeros
					if ((E[m[0]][i][0] - m[0]) * m[1]) > 0: # Se verifica si le sirve la dirección
						# Se agrega que verifique si el tren para en esa estación:
						#print(f"El pasajero va a la posicion {E[m[0]][i][0]} con color {L[E[m[0]][i][0]]}" )
						m[2].append(E[m[0]][i]) # Se agrega el pasajero a la lista
						E[m[0]].pop(i) # Se elimina el pasajero de la espera
					else:
						i += 1
				else:
					i += 1
			m[-2] = False # Necesariamente es False por que no hay 2 estaciones seguidas
		else:
			m[0] += m[1] # Se mueve uno de posicion
			if m[0] in [0, len(L)-1]: # Se verifica si esta en una posición borde
				m[1] *= -1 # Se cambia la dirección
			# Se verifica que la proxima sea una estación que le sirva 
			if (L[m[0]] == 1) or (L[m[0]] == m[4]): # Si la siguiente posicion es una estacion que le sirva debe ser True
				m[-2] = True

	print("\n----------------------------")
	print(f"Tiempo: {t}")
	print("\nMetros:")
	for m in M:
		d = 'derecha' if m[1] == 1 else 'izquierda'
		print(f"\tMetro en posición {m[0]} avanza hacia {d} con {len(m[2])} pasajeros")
	print("\nEstaciones:")
	for e in E:
		print(f"\tEstación en posición {e} tiene {len(E[e])} esperando en el andén")
	#input()

print(f"\nEl tiempo medio de viaje fue {sum(T)/len(T):0.1f} minutos")