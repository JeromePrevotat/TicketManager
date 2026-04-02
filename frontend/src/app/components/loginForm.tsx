'use client'
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import FormControl from '@mui/material/FormControl';
import IconButton from '@mui/material/IconButton';
import InputAdornment from '@mui/material/InputAdornment';
import SendIcon from '@mui/icons-material/Send';
import OutlinedInput from '@mui/material/OutlinedInput';
import TextField from '@mui/material/TextField';
import FormLabel from '@mui/material/FormLabel';
import { useState } from 'react';

export default function InputAdornments() {
  const [showPassword, setShowPassword] = useState(false);

  const handleClickShowPassword = () => setShowPassword((show) => !show);

  const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
  };

  const handleMouseUpPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
  };

  return (
    <Box
      component="form"
      sx={{ '& .MuiTextField-root': { m: 1, width: '25ch' } }}
      noValidate
      autoComplete="off"
    >
      <div className='formContainer'>
        <h1>Sign In</h1>
        <FormControl sx={{ m: 1, width: '25ch' }} variant="outlined">
          <FormLabel htmlFor="outlined-username-input">Username</FormLabel>
          <TextField
            id="outlined-username-input"
            type="text"
            autoComplete="username"
          />
          <FormLabel htmlFor="outlined-adornment-password">Password</FormLabel>
          <a href='#'>Forgot password ?</a>
          <OutlinedInput
            id="outlined-adornment-password"
            type={showPassword ? 'text' : 'password'}
            endAdornment={
              <InputAdornment position="end">
                <IconButton
                  aria-label={
                    showPassword ? 'hide the password' : 'display the password'
                  }
                  onClick={handleClickShowPassword}
                  onMouseDown={handleMouseDownPassword}
                  onMouseUp={handleMouseUpPassword}
                  edge="end"
                >
                  {showPassword ? <VisibilityOff /> : <Visibility />}
                </IconButton>
              </InputAdornment>
            }
            label="Password"
          />
          <Button variant="contained" endIcon={<SendIcon />}>
            Sign In
          </Button>
        </FormControl>
        <p>Don't have an account? <a href='#'>Sign Up</a></p>
      </div>
    </Box>
  );
}
