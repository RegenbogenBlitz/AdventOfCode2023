use std::fs;

fn main(){
    let filename = "puzzle1input.txt";
    match fs::read_to_string(filename)
    {
        Ok(contents)=>{
            let lines = contents.lines();
            let mut total = 0;
            for (_, line) in lines.enumerate() {
                let mut first_digit_char : Option<char> = None;
                let mut last_digit_char: Option<char> = None;
                for digit in line.chars().filter(|c| c.is_digit(10)){
                    if first_digit_char == None {
                        first_digit_char = Some(digit);
                    }
                    last_digit_char = Some(digit);
                }

                match first_digit_char
                {
                    Some(digit_char)=>{
                        total += 10 * digit_char.to_digit(10).unwrap();
                    },
                    None =>{}
                }
                
                match last_digit_char
                {
                    Some(digit_char)=>{
                        total += digit_char.to_digit(10).unwrap();
                    },
                    None =>{}
                }
            }
            
            println!("Total: {}", total);
        },
        Err(error)=>{
            println!("Failed to read file: {}", error);
        }
    }
}