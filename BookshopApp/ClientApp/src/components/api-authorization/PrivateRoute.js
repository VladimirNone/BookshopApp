import React, { Component } from 'react';
import { Route, Redirect } from 'react-router';
import authService from './AuthorizeService';


//This PrivateRoute was based on PrivateRoute from https://ui.dev/react-router-v5-protected-routes-authentication/
//Changed for async methods
export class PrivateRoute extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isAuthenticated: false,
        };
    }

    componentDidMount() {
        authService.isAuthenticated().then(res => this.setState({ isAuthenticated: res }));
    }

    render() {
        const isAuth = this.state.isAuthenticated;
        const { children, ...rest } = this.props;

        return <Route {...rest} render={() => { return isAuth ? children : <Redirect to='/' /> }} />
    }
}

