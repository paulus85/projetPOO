#pragma once

#ifdef WANTDLLEXP
#define DLL _declspec(dllexport)
#define EXTERNC extern "C"
#else
#define DLL
#define EXTERNC
#endif

#include <map>
#include <vector>
#include "Enum.h"
#include "Point.h"

using namespace std;

class DLL SuggestionCase {
private:
	Case** carte;
	int taille;
	Peuple* peuples;

	int getScoreMouvement(Point pos, Peuple peuple) const;
	int getScoreCapture(Joueur occupant, Joueur joueur, Peuple peuple) const;

public:
	
	EXTERNC DLL SuggestionCase(Case** carte, int taille, Peuple PeupleJoueur1, Peuple PeupleJoueur2);
	EXTERNC DLL Point* getSuggestion(int x, int y, Joueur** unites, Joueur joueur) const;
};

