import React, { Component, Fragment } from 'react';
import { Link } from 'react-router-dom';
import { ApplicationPaths } from './Api-authorization/ApiConstants';
import authService from './Api-authorization/AuthorizeService';

export class Login extends Component {
    static displayName = Login.name;

    constructor(props){
        super(props);
        this.state = {
            login: '',
            password: '',
            success: false,
            errors: [],
            redirect: false
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
    }

    handleSubmit(e) {
        e.preventDefault();

        authService.login(this.state.login, this.state.password, () => this.setState({ redirect: true }), (errors) => this.setState({ errors: errors }));
    }

    handleInputChange(e) {
        this.setState({
            [e.target.name]: e.target.value
        });
    }

    render() {
        if (this.state.redirect) {
            window.location.replace(`${window.location.origin}/`);
            //need render all page, because LoginMenu don't render, when use redirect
            //return (<Redirect to={'/'} />);
        }

        return (
            <Fragment>
                <h1>Log in</h1>
                <div className="row">
                    <div className="col-md-4">
                        <section>
                            <form onSubmit={this.handleSubmit}>
                                <h2>Use a local account to log in.</h2>
                                <hr />
                                <div className="text-danger"></div>
                                    {this.state.errors.map((error, i) => <p key={i}>{error}</p>)}
                                <div className="form-group">
                                    <label>Email</label>
                                    <input name="login" className="form-control" onChange={this.handleInputChange} value={this.state.login} />
                                    <span className="text-danger"></span>
                                </div>
                                <div className="form-group">
                                    <label>Password</label>
                                    <input name="password" className="form-control" onChange={this.handleInputChange} value={this.state.password} />
                                    <span className="text-danger"></span>
                                </div>
                                <div className="form-group">
                                    <button type="submit" className="btn btn-primary">Log in</button>
                                </div>
                                <div className="form-group">
                                    <p>
                                        <Link to={ApplicationPaths.Register}>Register as a new user</Link>
                                    </p>
                                </div>
                            </form>
                        </section>
                    </div>
                </div>
            </Fragment>
        );
    }
}


