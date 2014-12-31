#pragma once

#include "Enum.h"

class Point {

public:
	int x;
	int y;
	Point(int x, int y);
	Point();
	bool operator==(const Point& pt) const;
	bool operator<(const Point& pt) const;
	bool estValide(int size) const;
	bool estMontagne(Case** map) const;
};