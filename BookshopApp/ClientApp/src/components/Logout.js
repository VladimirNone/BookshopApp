import React from 'react';
import { Redirect } from 'react-router';
import authService from './api-authorization/AuthorizeService';

export function Logout() {
    authService.logout();
    window.location.replace(`${window.location.origin}/`);
    //need render all page, because LoginMenu don't render, when use Redirect
    return (<Redirect to="/"  />)
}