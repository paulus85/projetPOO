#ifdef WANTDLLEXP
#define DLL _declspec(dllexport)
#define EXTERNC extern "C"
#else
#define DLL
#define EXTERNC
#endif

class DLL Carte {

	int** _carte;

public:
	Carte(int nbCases);
	~Carte();

	int** getCarte();
};

// A ne pas implémenter dans le .h !
//EXTERNC DLL Carte* Carte_Demo();

