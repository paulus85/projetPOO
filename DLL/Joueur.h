#ifdef WANTDLLEXP
#define DLL _declspec(dllexport)
#define EXTERNC extern "C"
#else
#define DLL
#define EXTERNC
#endif

class DLL Joueur {

	char _nom;
	int _x;
	int _y;

public:
	Joueur(char nom);
	~Joueur();

	void setCoordonn�es(int x, int y);
};