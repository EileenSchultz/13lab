# Практика 13

Нужно сделать круд эндпоинты для новой сущности Book, представляющей собой запись данные о книге в библиотеке

<div class="tableWrapper"><div class="tableWrapper-inner"><table style="--default-cell-min-width: 120px; min-width: 360px;"><colgroup><col><col><col></colgroup><tbody><tr><td colspan="1" rowspan="1"><p>Колонка</p></td><td colspan="1" rowspan="1"><p>Тип C#</p></td><td colspan="1" rowspan="1"><p>Nullable</p></td></tr><tr><td colspan="1" rowspan="1"><p>Id</p></td><td colspan="1" rowspan="1"><p>int</p></td><td colspan="1" rowspan="1"><p>-</p></td></tr><tr><td colspan="1" rowspan="1"><p>Name</p></td><td colspan="1" rowspan="1"><p>string</p></td><td colspan="1" rowspan="1"><p>-</p></td></tr><tr><td colspan="1" rowspan="1"><p>Author</p></td><td colspan="1" rowspan="1"><p>string</p></td><td colspan="1" rowspan="1"><p>-</p></td></tr><tr><td colspan="1" rowspan="1"><p>ReleaseDate</p></td><td colspan="1" rowspan="1"><p>DateOnly</p></td><td colspan="1" rowspan="1"><p>+</p></td></tr></tbody></table></div><div class="table-widgets-container" style="position: relative;"></div></div>

Настройка подключения к БД теперь выполняется из файла настроек - appsettings.json.

В случае запуска через тесты не используется та же бд, что и при разработке, вместо этого используется in-memory БД SQLite.
