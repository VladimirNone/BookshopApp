import React, { Component, Fragment } from 'react';
import { Redirect } from 'react-router';
import { ApplicationApiPaths } from './Api-authorization/ApзConstants';
import authService from './Api-authorization/AuthorizeService';

export class Signup extends Component {
    constructor(props) {
        super(props);
        this.state = {
            login: '',
            password: '',
            confirmPassword: '',
            success: false,
            errors: [],
            redirect: false
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
    }

    handleSubmit(e) {
        e.preventDefault();

        const password = this.state.password;
        const confirmPassword = this.state.confirmPassword;
        const login = this.state.login;

        if (password !== confirmPassword) {
            this.setState({ errors: ["You must write the same passwords"] })
            this.setState({ login: '', password: '', confirmPassword: '' });
            return;
        }

        authService.signup(login, password, () => this.setState({ redirect: true }), (errors) => this.setState({ errors: errors }));
    }

    handleInputChange(e) {
        this.setState({
            [e.target.name]: e.target.value
        });
    }

    render() {
        if (this.state.redirect) {
            return (<Redirect to={ApplicationApiPaths.Login} />);
        }

        return (
            <Fragment>
                <h1>Sign up</h1>
                <div className="row">
                    <div className="col-md-4">
                        <form onSubmit={this.handleSubmit}>
                            <h2>Create a new account.</h2>
                            <hr />
                            <div className="text-danger">
                                {this.state.errors.map((error,i) => <p key={i}>{error}</p>)}
                            </div>
                            <div className="form-group">
                                <label>Login</label>
                                <input name="login" className="form-control" onChange={this.handleInputChange} value={this.state.login} />
                                <span className="text-danger"></span>
                            </div>
                            <div className="form-group">
                                <label>Password</label>
                                <input name="password" className="form-control" onChange={this.handleInputChange} value={this.state.password} />
                                <span className="text-danger"></span>
                            </div>
                            <div className="form-group">
                                <label>Confirm password</label>
                                <input name="confirmPassword" className="form-control" onChange={this.handleInputChange} value={this.state.confirmPassword} />
                                <span className="text-danger"></span>
                            </div>
                            <button type="submit" className="btn btn-primary">Register</button>
                        </form>
                    </div>
                </div>
            </Fragment>
        );
    }
}



