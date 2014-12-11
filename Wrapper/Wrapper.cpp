#include "Wrapper.h"

/**
* Generates a random map.
* @param size The size of the map to generate.
* @returns A matrix of integer representing the different types of squares.
*/
array<array<int>^>^ Wrapper::Wrapper::genererCarte(int size) {
	Tuile** result = GenerateurCarte::genererCarte(size);

	// Convert the result from the C++ library into a matrix readable by C#:
	array<array<int>^>^ map = gcnew array<array<int>^>(size);
	for (int i = 0; i<size; i++) {
		map[i] = gcnew array<int>(size);
		for (int j = 0; j<size; j++) {
			map[i][j] = result[i][j];
		}
	}
	return map;
}

/**
* Places units of the two player on the map.
* @param map The map as a matrix of integer.
* @param size The size of the map.
* @returns A matrix with the coordinate of the units for the two players.
*/
array<array<int>^>^ Wrapper::Wrapper::placementJoueur(array<array<int>^>^ map, int size) {
	Tuile** result = new Tuile*[size];
	for (int i = 0; i<size; i++) {
		result[i] = new Tuile[size];
		for (int j = 0; j<size; j++) {
			result[i][j] = (Tuile)map[i][j];
		}
	}
	Point* starts = GenerateurCarte::placerUnités(result, size);

	array<array<int>^>^ pos = gcnew array<array<int>^>(2);
	for (int i = 0; i<2; i++) {
		pos[i] = gcnew array<int>(2);
		pos[i][0] = starts[i].x;
		pos[i][1] = starts[i].y;
	}
	return pos;
}