(() => {
    const defaultValue =
        "2413432311323\n" +
        "3215453535623\n" +
        "3255245654254\n" +
        "3446585845452\n" +
        "4546657867536\n" +
        "1438598798454\n" +
        "4457876987766\n" +
        "3637877979653\n" +
        "4654967986887\n" +
        "4564679986453\n" +
        "1224686865563\n" +
        "2546548887735\n" +
        "4322674655533";

    const input = document.querySelector('#input_textarea');
    input.value = defaultValue
})();

const Direction = {
    Start: 0,
    Right: 1,
    Up: 2,
    Left: 3,
    Down: 4
};

const createCreateNewCell = (createStates) => (x, y, cost) => {
    const states = [];
    const cell = {
        x: x,
        y: y,
        cost: cost,
        states: states
    };

    const createState = (createPossibleDirections) => (direction, distance) => ({
        cell: cell,
        direction: direction,
        distance: distance,
        minValue: null,
        fromState: null,
        directions: createPossibleDirections(direction, distance),
        nextStates: [],
        bestPath: null
    })

    createStates(createState, states, x, y);

    return cell;
}

const generalCreateStates = (minDistance, maxDistance) => (maxColumn, maxRow) => (createCreateState, states, x, y) => {
    const createPossibleDirections = (direction, distance) => {
        const possibleDirections = [];
        if (y != maxRow &&
            !(direction == Direction.Down && distance >= maxDistance) &&
            !(direction == Direction.Left && distance < minDistance) &&
            !(direction == Direction.Right && distance < minDistance) &&
            direction != Direction.Up) {
            possibleDirections.push(Direction.Down);
        }
        if (y != 0 &&
            !(direction == Direction.Up && distance >= maxDistance) &&
            !(direction == Direction.Left && distance < minDistance) &&
            !(direction == Direction.Right && distance < minDistance) &&
            direction != Direction.Down) {
            possibleDirections.push(Direction.Up);
        }
        if (x != maxColumn &&
            !(direction == Direction.Right && distance >= maxDistance) &&
            !(direction == Direction.Up && distance < minDistance) &&
            !(direction == Direction.Down && distance < minDistance) &&
            direction != Direction.Left) {
            possibleDirections.push(Direction.Right);
        }
        if (x != 0 &&
            !(direction == Direction.Left && distance >= maxDistance) &&
            !(direction == Direction.Up && distance < minDistance) &&
            !(direction == Direction.Down && distance < minDistance) &&
            direction != Direction.Right) {
            possibleDirections.push(Direction.Left);
        }

        return possibleDirections;
    }

    const createState = createCreateState(createPossibleDirections);

    if (y == 0 && x == 0) {
        states.push(createState(Direction.Start, 0));
    }

    for (let i = 0; i < maxDistance; i++) {
        if (y > i) {
            states.push(createState(Direction.Down, i + 1));
        }
    }

    for (let i = 0; i < maxDistance; i++) {
        if (y < maxRow - i) {
            states.push(createState(Direction.Up, i + 1));
        }
    }

    for (let i = 0; i < maxDistance; i++) {
        if (x > i) {
            states.push(createState(Direction.Right, i + 1));
        }
    }

    for (let i = 0; i < maxDistance; i++) {
        if (x < maxColumn - i) {
            states.push(createState(Direction.Left, i + 1));
        }
    }
}


const day17_part1 = () => {
    const minDistance = 1;
    const maxDistance = 3;

    day17_run(generalCreateStates(minDistance, maxDistance));
}

const day17_part2 = () => {
    const minDistance = 4;
    const maxDistance = 10;

    day17_run(generalCreateStates(minDistance, maxDistance));
}

const day17_run = (createStates) => {
    const output = document.querySelector('#output');
    output.innerHTML = '';

    const input = document.querySelector('#input_textarea');
    const inputValue = input.value.trim();
    const lines = inputValue.split('\n');
    const numColumns = lines[0].length;
    const maxColumn = numColumns - 1;
    for (let i = 1; i < lines.length; i++) {
        if (lines[i].length != numColumns) {
            output.innerHTML = "Error: not all lines are the same length";
            return;
        }
    }
    const numRows = lines.length;
    const maxRow = numRows - 1;

    // Create the cells
    const createNewCell = createCreateNewCell(createStates(maxColumn, maxRow));

    const cells = [];
    for (let y = 0; y < numRows; y++) {
        cells.push([]);
        for (let x = 0; x < numColumns; x++) {
            const cellCost = parseInt(lines[y][x]);
            const cell = createNewCell(x, y, cellCost);
            cells[y].push(cell);
        }
    }

    // Create the graph
    for (let y = 0; y < numRows; y++) {
        cells.push([]);
        for (let x = 0; x < numColumns; x++) {
            const cell = cells[y][x];
            const states = cell.states;
            for (let i = 0; i < states.length; i++) {
                const state = states[i];
                const directions = state.directions;
                for (let j = 0; j < directions.length; j++) {
                    const direction = directions[j];
                    let nextX = x;
                    let nextY = y;
                    let nextDistance = 1;
                    switch (direction) {
                        case Direction.Right:
                            nextX++;
                            if (state.direction == Direction.Right) {
                                nextDistance = state.distance + 1;
                            }
                            break;
                        case Direction.Up:
                            nextY--;
                            if (state.direction == Direction.Up) {
                                nextDistance = state.distance + 1;
                            }
                            break;
                        case Direction.Left:
                            nextX--;
                            if (state.direction == Direction.Left) {
                                nextDistance = state.distance + 1;
                            }
                            break;
                        case Direction.Down:
                            nextY++;
                            if (state.direction == Direction.Down) {
                                nextDistance = state.distance + 1;
                            }
                            break;
                    }
                    const nextState = cells[nextY][nextX].states.find(s =>
                        s.direction == direction && s.distance == nextDistance);

                    if (nextState) {
                        if (state.nextStates == null) {
                            state.nextStates = [];
                        }
                        state.nextStates.push(nextState);
                        nextState.fromState = state;
                    } else {
                        throw new Error("Next state not found");
                    }
                }
            }
        }
    }

    // Draw the table
    const createTable = (idPrefix) => {
        const table = document.createElement('table');
        for (let y = 0; y < numRows; y++) {
            const row = document.createElement('tr');
            for (let x = 0; x < numColumns; x++) {
                const cell = document.createElement('td');
                cell.id = `${idPrefix}_${x}_${y}`;
                cell.innerHTML = cells[y][x].cost;
                row.appendChild(cell);
            }
            table.appendChild(row);
        }
        output.appendChild(table);
    }

    createTable('cell');

    // Find the shortest path
    const startCell = cells[0][0];
    const startState = startCell.states.find(s => s.direction == Direction.Start && s.distance == 0);
    startState.bestPath = [startState];
    startState.minValue = 0;

    const queue = [];
    queue.push(startState);

    while (queue.length > 0) {
        const state = queue.shift();

        if (state.nextStates == null) {
            const dummy = 0;
        }

        for (let i = 0; i < state.nextStates.length; i++) {
            const nextState = state.nextStates[i];

            const newValue = state.minValue + nextState.cell.cost;
            if (nextState.minValue == null || newValue < nextState.minValue) {
                nextState.minValue = newValue;
                nextState.bestPath = state.bestPath.concat(nextState);
                queue.push(nextState);
            }
        }
    }



    const endCell = cells[maxRow][maxColumn];
    // find the end cell state with the minimum value
    let minState = null;
    for (let i = 0; i < endCell.states.length; i++) {
        const state = endCell.states[i];
        if (state.minValue != null && (minState == null || state.minValue < minState.minValue)) {
            minState = state;
        }
    }


    // Highlight the best path
    let bestPathDescription = '';
    for (let i = 0; i < minState.bestPath.length; i++) {
        const state = minState.bestPath[i];
        const cell = state.cell;
        const cellElement = document.querySelector(`#cell_${cell.x}_${cell.y}`);
        cellElement.style.backgroundColor = 'yellow';

        bestPathDescription += `(${cell.x}, ${cell.y}) -> `;
    }

    output.innerHTML += `<p>Min value: ${minState.minValue}</p>`;
}
