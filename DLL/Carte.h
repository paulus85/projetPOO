#ifdef WANTDLLEXP
#define DLL _declspec(dllexport)
#define EXTERNC extern "C"
#else
#define DLL
#define EXTERNC
#endif

#include "Enum.h"
#include "Joueur.h"
#include <stdlib.h>
#include <time.h>

class DLL Carte {

	Tuile** _carte;

public:
	Carte(int nbCases, Joueur* j1, Joueur* j2);
	~Carte();

	Tuile** getCarte();
};

// A ne pas implémenter dans le .h !
EXTERNC DLL Carte* Carte_new(int nbCases);
EXTERNC DLL void Carte_delete(Carte* carte);
EXTERNC DLL Tuile** GetCarte(Carte* carte);


