"""A reduced reconstruction of sequential C# console execution.

The playback holds make teaching states readable; they do not model CPU time.
The C# sample deliberately contains no delays or additional statements.
"""

from manim import (
    Scene, Text, VGroup, RoundedRectangle, Rectangle, Line,
    FadeIn, FadeOut, LEFT,
)


class CourseAnimation(Scene):
    """Show active -> output appended -> completed for each source statement."""

    def construct(self):
        self.camera.background_color = "#101827"
        ink = "#EEF3FC"
        muted = "#AABBD2"
        accent = "#F6C85F"
        done = "#75DEB3"
        border = "#40536E"
        mono = "DejaVu Sans Mono"
        sans = "DejaVu Sans"

        def label(text, size, color=ink, font=sans):
            return Text(
                text, font=font, font_size=size, color=color,
                disable_ligatures=True,
            )

        title = label("Sequential execution", 34)
        title.move_to([0, 3.24, 0])

        source_panel = RoundedRectangle(
            width=8.2, height=4.8, corner_radius=0.16,
            stroke_color=border, stroke_width=1.5,
            fill_color="#182438", fill_opacity=1,
        ).move_to([-2.5, 0.2, 0])
        console_panel = RoundedRectangle(
            width=3.8, height=4.8, corner_radius=0.16,
            stroke_color=border, stroke_width=1.5,
            fill_color="#111E2D", fill_opacity=1,
        ).move_to([4.5, 0.2, 0])
        source_heading = label("Program.cs", 24)
        source_heading.move_to([-6.22, 2.18, 0], aligned_edge=LEFT)
        console_heading = label("Console output", 24)
        console_heading.move_to([2.94, 2.18, 0], aligned_edge=LEFT)
        rules = VGroup(
            Line([-6.22, 1.8, 0], [1.22, 1.8, 0], color=border),
            Line([2.94, 1.8, 0], [6.06, 1.8, 0], color=border),
        )

        statements = [
            'Console.WriteLine("Hello world!");',
            "Console.WriteLine(1);",
            "Console.WriteLine(2);",
            "Console.WriteLine(3);",
        ]
        values = ["Hello world!", "1", "2", "3"]
        row_y = [1.06, 0.21, -0.64, -1.49]
        code_rows = VGroup()
        numbers = VGroup()
        output_rows = []
        checks = []
        for index, (statement, value, y) in enumerate(
            zip(statements, values, row_y)
        ):
            code = label(statement, 22, font=mono)
            code.move_to([-5.76, y, 0], aligned_edge=LEFT)
            code_rows.add(code)
            number = label(str(index + 1), 20, muted, mono)
            number.move_to([-6.16, y, 0])
            numbers.add(number)
            output = label(value, 22, font=mono)
            output.move_to([2.96, y, 0], aligned_edge=LEFT)
            output_rows.append(output)
            check = label("✓", 24, done)
            check.move_to([1.28, y, 0])
            checks.append(check)

        empty = label("No output yet", 22, muted)
        empty.move_to([2.96, row_y[0], 0], aligned_edge=LEFT)
        footer = label("Illustrative pacing • not CPU time", 18, muted)
        footer.move_to([0, -3.33, 0])
        status = label("Ready", 27)
        status.move_to([0, -2.7, 0])

        def set_state(text, color=ink):
            nonlocal status
            self.remove(status)
            status = label(text, 27, color)
            status.move_to([0, -2.7, 0])
            self.add(status)

        self.add(
            title, source_panel, console_panel, source_heading,
            console_heading, rules, code_rows, numbers, empty, footer, status,
        )
        self.wait(1.0)

        for index, y in enumerate(row_y):
            # One indicator exists at a time. The previous one has been removed
            # and its completed state held before this iteration can begin.
            active = Rectangle(
                width=7.52, height=0.48,
                stroke_color=accent, stroke_width=2.5,
                fill_color=accent, fill_opacity=0.09,
            ).move_to([-2.5, y, 0])
            active.set_z_index(1)
            code_rows.set_z_index(2)
            numbers.set_z_index(2)
            set_state(f"{index + 1} · executing", accent)
            self.play(FadeIn(active), run_time=0.2)
            self.wait(0.3)

            # Copy the literal glyphs without changing the complete code line.
            # A separate lane above each row keeps the travelling copy clear
            # of other statements and previously appended console output.
            value_start = 19 if index == 0 else 18
            literal = code_rows[index][value_start:value_start + len(values[index])]
            travelling_value = literal.copy().set_color(accent).set_opacity(0)
            travelling_value.set_z_index(3)
            self.add(travelling_value)
            set_state(f"{index + 1} · printing", accent)
            self.play(
                travelling_value.animate.shift([0, 0.4, 0]).set_opacity(1),
                run_time=0.2,
            )
            self.play(
                travelling_value.animate.move_to(
                    output_rows[index].get_center() + [0, 0.4, 0]
                ),
                run_time=0.6,
            )
            if index == 0:
                self.remove(empty)
            self.play(
                travelling_value.animate.move_to(
                    output_rows[index].get_center()
                ).set_color(ink),
                run_time=0.2,
            )
            self.remove(travelling_value)
            self.add(output_rows[index])
            set_state(f"{index + 1} · output appended", accent)
            self.wait(0.4)

            set_state(f"{index + 1} · completed", done)
            self.play(
                FadeOut(active),
                code_rows[index].animate.set_color(done),
                FadeIn(checks[index]),
                run_time=0.2,
            )
            self.wait(0.4)

        set_state("Program complete", done)
        self.wait(1.0)
