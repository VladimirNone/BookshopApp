import React, { Component, Fragment } from 'react';
import { NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import authService from '../Api-authorization/AuthorizeService';
import { AppPagePaths } from '../Api-authorization/AppConstants';

export class LoginMenu extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isAuthenticated: false,
            userName: null
        };
    }

    componentDidMount() {
        this.populateState();
    }

    async populateState() {
        const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()])
        this.setState({
            isAuthenticated,
            userName: user.username
        });
    }

    render() {
        const { isAuthenticated, userName } = this.state;
        if (!isAuthenticated) {
            const registerPath = `${AppPagePaths.Register}`;
            const loginPath = `${AppPagePaths.Login}`;
            return this.anonymousView(registerPath, loginPath);
        } else {
            const profilePath = `${AppPagePaths.Profile}`;
            const logoutPath = { pathname: `${AppPagePaths.Logout}`, state: { local: true } };
            return this.authenticatedView(userName, profilePath, logoutPath);
        }
    }

    authenticatedView(userName, profilePath, logoutPath) {
        return (<Fragment>
            <NavItem>
                <NavLink tag={Link} className="text-dark" to={AppPagePaths.Cart}>Корзина</NavLink>
            </NavItem>
            <NavItem>
                <NavLink tag={Link} className="text-dark" to={AppPagePaths.Orders}>Заказы</NavLink>
            </NavItem>
            <NavItem>
                <NavLink tag={Link} className="text-dark" to={profilePath}>Привет {userName}</NavLink>
            </NavItem>
            <NavItem>
                <NavLink tag={Link} className="text-dark" to={logoutPath}>Выйти</NavLink>
            </NavItem>
        </Fragment>);
    }

    anonymousView(registerPath, loginPath) {
        return (<Fragment>
            <NavItem>
                <NavLink tag={Link} className="text-dark" to={registerPath}>Регистрация</NavLink>
            </NavItem>
            <NavItem>
                <NavLink tag={Link} className="text-dark" to={loginPath}>Войти</NavLink>
            </NavItem>
        </Fragment>);
    }
}
