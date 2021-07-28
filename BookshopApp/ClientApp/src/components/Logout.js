import authService from './Api-authorization/AuthorizeService';

export function Logout() {
    authService.logout();
    window.location.replace(`${window.location.origin}/`);
    //need render all page, because LoginMenu don't render, when use Redirect
    //return (<Redirect to="/"  />)
}