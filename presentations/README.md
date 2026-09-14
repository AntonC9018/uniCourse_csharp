# Software design lessons

Two Slidev presentations with C# examples and private presenter notes.

## Run a lesson

Install Node.js 22.12 or newer. Open a terminal in this folder and run:

```sh
npm ci
npm run dev
```

This opens lesson 1. For lesson 2, stop the first command with Ctrl+C and run:

```sh
npm run lesson2
```

For the complete reference implementations:

```sh
npm run reference
```

The terminal prints the audience and presenter addresses. Open presenter mode from the bottom-left toolbar, or add `/presenter` to the local address.

Use extended display mode: put the audience browser window on the projector and keep the presenter window on your laptop. For a video call, share only the audience window. Presenter navigation synchronizes the audience window. Arrow keys advance; `f` toggles fullscreen.

## Edit the slides

- `lesson-1.md`: 21 slides, including the cover.
- `lesson-2.md`: 25 slides, including the cover.
- `reference.md`: 11 reference slides, including the cover.
- `style.css`: shared typography and colors.
- `global-bottom.vue`: slide numbering.
- `source-notes.md`: original review document, preserved for context.
- `slide-map.json`: correspondence between slides and original sections.

The lesson files are the editable presentation sources. A line containing `---` separates slides. The HTML comment at the end of each slide contains its presenter notes and stays off the audience screen.

Each section is one slide: related examples and their questions sit side by side in two columns so the context stays visible. The HTML comment at the end of each slide contains its presenter notes and stays off the audience screen. The stated teaching time belongs to the section as a whole. The reference deck and source document retain the supporting implementations.

Code uses C# highlighting. Existing `diff` blocks show additions and removals. To emphasize selected lines on successive clicks, change a fence from `csharp` to `csharp {all|2-4|all}`.

## Export or build

```sh
npm run export:lesson1
npm run export:lesson2
npm run build
```

PDF export may require a browser download on first use. If requested by Playwright, run `npx playwright install chromium`.

Build output goes into `dist/lesson-1` and `dist/lesson-2`. These are separate web builds intended to be served individually at a web root, not opened directly as local HTML files. You do not need to build to present locally.

Official guides: [Slidev setup](https://sli.dev/guide/), [presenter mode](https://sli.dev/guide/ui#presenter-mode), [Markdown and notes](https://sli.dev/guide/syntax), [line highlighting](https://sli.dev/features/line-highlighting).
