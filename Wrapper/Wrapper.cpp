#include "Wrapper.h"

array<array<int>^>^ Wrapper::Wrapper::genererCarte(int taille) {
	Case** result = GenerateurCarte::genererCarte(taille);

	//Converti le résultat de la librairie C++ dans une matrice lisible par C#
	array<array<int>^>^ carte = gcnew array<array<int>^>(taille);
	for (int i = 0; i<taille; i++) {
		carte[i] = gcnew array<int>(taille);
		for (int j = 0; j<taille; j++) {
			carte[i][j] = result[i][j];
		}
	}
	return carte;
}


array<array<int>^>^ Wrapper::Wrapper::placementJoueur(int taille) {
	Point* starts = GenerateurCarte::departUnites(taille);

	array<array<int>^>^ pos = gcnew array<array<int>^>(2);
	for (int i = 0; i<2; i++) {
		pos[i] = gcnew array<int>(2);
		pos[i][0] = starts[i].x;
		pos[i][1] = starts[i].y;
	}
	return pos;
}


array<array<int>^>^ Wrapper::Wrapper::getSuggestion(array<array<int>^>^ carte, int taille, int peupleJoueur1, int peupleJoueur2, int x, int y, array<array<int>^>^ unites, array<array<double>^>^ ptsDeplacement, int joueur) {
	Case** carteBis = new Case*[taille];
	for (int i = 0; i<taille; i++) {
		carteBis[i] = new Case[taille];
		for (int j = 0; j<taille; j++) {
			carteBis[i][j] = (Case)carte[i][j];
		}
	}
	SuggestionCase generator = SuggestionCase(carteBis, taille, (Peuple)peupleJoueur1, (Peuple)peupleJoueur2);

	Joueur** unitesBis = new Joueur*[taille];
	for (int i = 0; i<taille; i++) {
		unitesBis[i] = new Joueur[taille];
		for (int j = 0; j<taille; j++) {
			unitesBis[i][j] = (Joueur)unites[i][j];
		}
	}

	double** ptsDeplacementBis = new double*[taille];
	for (int i = 0; i<taille; i++) {
		ptsDeplacementBis[i] = new double[taille];
		for (int j = 0; j<taille; j++) {
			ptsDeplacementBis[i][j] = ptsDeplacement[i][j];
		}
	}

	Point* advice = generator.getSuggestion(x, y, unitesBis, ptsDeplacementBis, (Joueur)joueur);

	array<array<int>^>^ result = gcnew array<array<int>^>(3);
	for (int i = 0; i < 3; i++) {
		result[i] = gcnew array<int>(2);
		result[i][0] = advice[i].x;
		result[i][1] = advice[i].y;
	}
	return result;
}