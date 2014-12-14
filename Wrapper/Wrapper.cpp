#include "Wrapper.h"

array<array<int>^>^ Wrapper::Wrapper::genererCarte(int size) {
	Case** result = GenerateurCarte::genererCarte(size);

	//Converti le résultat de la librairie C++ dans une matrice lisible par C#
	array<array<int>^>^ map = gcnew array<array<int>^>(size);
	for (int i = 0; i<size; i++) {
		map[i] = gcnew array<int>(size);
		for (int j = 0; j<size; j++) {
			map[i][j] = result[i][j];
		}
	}
	return map;
}


array<array<int>^>^ Wrapper::Wrapper::placementJoueur(int size) {
	Point* starts = GenerateurCarte::departUnites(size);

	array<array<int>^>^ pos = gcnew array<array<int>^>(2);
	for (int i = 0; i<2; i++) {
		pos[i] = gcnew array<int>(2);
		pos[i][0] = starts[i].x;
		pos[i][1] = starts[i].y;
	}
	return pos;
}