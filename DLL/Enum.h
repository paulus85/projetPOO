#pragma once

/**
* \brief     Type de terain possible pour une case.
* \details   La table de correspondance des cases est la m�me que celle d�inie dans le code C#.
*/
const enum Case {
	PLAINE = 0,
	DESERT,
	MONTAGNE,
	FORET,
	MARAIS
};

/**
* \brief     Type de peuple possible pour une unit�.
* \details   La table de correspondance des unit�s est la m�me que celle d�inie dans le code C#.
*/
const enum Peuple {
	ELF = 0,
	NAIN,
	ORC,
	ZOMBIE
};