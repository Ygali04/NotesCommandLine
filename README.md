# NotesCommandLine
Hey There. This is my first solo full-stack project that I will be building from scratch. I feel it's ambitious regarding the amount of modules, software, 
and languages I will be using. _**This ReadMe will be continuously updated as time passes**_


The purpose is to build a multi-user authenticated Note Taking App with database storage and an interactive GUI. Below is a list of features that I will be updating as 
I further build the app.

## Original Features:
Only C#; crud operations directly manipulated files on the user’s system (in special folder called Notes) which allowed for permanent storage. But we want a database.

**FIRST COMMIT PUSHED TO GITHUB**

## Current Dev:
Frontend: Java Landing page (?)
Backend: 
- C# .NET crud operations for Note app (new, read, edit, delete, shownotes, directory)
- Postgres for monitoring and storing notes in note_db rather than creating files in user documents
- PgAdmin4 GUI (possibly react.js in future?)
- Ngsql integration with .NET for postgres server control
- db is directly manipulated by cruds

## Future:
- User auth + multi-user platform w/ user_db (primary key) that links to note_db i.e. each unique user had stored notes
- Cloud Hosting (redis, AWS, azure) for Ad-hoc queries
- Mobile dev (ios/android) via flutter
- LLM integration (kubernetes + openai) for note completion


