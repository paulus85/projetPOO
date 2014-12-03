#ifdef WANTDLLEXP
#define DLL _declspec(dllexport)
#define EXTERNC extern "C"
#else
#define DLL
#define EXTERNC
#endif

class DLL Carte {

	int tab[6][6];

public:
	Carte(int nbCases) {}
	~Carte() {}
};

// A ne pas implémenter dans le .h !
EXTERNC DLL Carte* Carte_Demo(int nbCases);