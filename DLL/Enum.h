#pragma once

/**
* \brief     Type de terain possible pour une case.
* \details   La table de correspondance des cases est la même que celle déinie dans le code C#.
*/
const enum Case {
	PLAINE = 0,
	DESERT,
	MONTAGNE,
	FORET,
	MARAIS
};

/**
* \brief     Type de peuple possible pour une unité.
* \details   La table de correspondance des unités est la même que celle déinie dans le code C#.
*/
const enum Peuple {
	ELF = 0,
	NAIN,
	ORC
};

/**
* \brief     Type de joueur possible au cours d'une partie.
* \details   Correspond au compteur static de la classe Joueur lors de la création d'un joueur
*/
const enum Joueur {
	NONE = 0,
	JOUEUR1,
	JOUEUR2
};