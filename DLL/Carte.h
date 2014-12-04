#ifdef WANTDLLEXP
#define DLL _declspec(dllexport)
#define EXTERNC extern "C"
#else
#define DLL
#define EXTERNC
#endif

class DLL CarteDemo {

	int** _carte;

public:
	CarteDemo() {}
	~CarteDemo() {}

	int** carte();
};

// A ne pas implémenter dans le .h !
EXTERNC DLL CarteDemo* Carte_Demo();

