CREATE TABLE "rss_item" (
	"Id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"Source"	INTEGER NOT NULL DEFAULT 0,
	"Title"	TEXT NOT NULL DEFAULT '',
	"Link"	TEXT NOT NULL DEFAULT '',
	"Date"	INTEGER NOT NULL DEFAULT CURRENT_TIMESTAMP
)