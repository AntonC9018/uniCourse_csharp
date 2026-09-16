# C# Course

Source material for the C# course, published as a navigable course site without making the website format the authoring format.

## Language

**Course repository**:
A repository containing the source material for one course and owning one independently deployed course site.
_Avoid_: Documentation repository, website repository

**Course site**:
The public website published from one course repository.
_Avoid_: Portal, aggregate site

**Content configuration**:
The repository-owned selection of language content roots and explicit inclusion or exclusion patterns that determines which Markdown source documents are lessons.
_Avoid_: Lesson manifest, website configuration

**Lesson**:
A Markdown source document selected for publication by this course repository's content configuration. Only documents under the configured language roots are lessons; slide decks and code examples are not.
_Avoid_: Page, article, slide deck

**Lab lesson**:
A lesson whose canonical hierarchy places it inside a labs group.
_Avoid_: Exercise, practical lesson

**Lesson slug**:
The stable, language-prefixed hierarchical identity of a lesson within its course site. The language prefix stays in the route even when the counterpart language has no document.
_Avoid_: Filename, source path, generated path

**Language counterpart**:
A lesson in another configured language whose source path is otherwise identical after removing the language root. Counterparts are optional: Russian lessons exist without English counterparts.
_Avoid_: Translation fallback, similar lesson

**Source document**:
The repository-readable Markdown file that authors edit and that remains the source of truth for a lesson.
_Avoid_: Website Markdown, generated document

**Course maintenance**:
The shared, course-agnostic submodule pinned at an explicit revision that provides lab upkeep and publishing commands. It is bumped manually, never floating.
_Avoid_: Per-task scripts, floating tooling
