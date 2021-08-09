import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { LoginMenu } from './SubComponents/LoginMenu';
import './NavMenu.css';
import { AppPagePaths } from './Api-authorization/AppConstants';
import authService from './Api-authorization/AuthorizeService';
import 'bootstrap';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true,
            access: false,
        };
    }

    componentDidMount() {
        this.getPermission();
    }

    async getPermission() {
        authService.checkPermission((res) => this.setState({ access: res.access }), (error) => console.error("NavMenu. getPermission()"));
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    render() {
        return (
            <header>
                <Navbar className="navbar-expand-lg navbar-toggleable-lg ng-white border-bottom box-shadow mb-3" light>
                    <Container>
                        <NavbarBrand tag={Link} to="/">BookshopApp</NavbarBrand>
                        <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                        <Collapse className="d-lg-inline-flex flex-lg-row-reverse" isOpen={!this.state.collapsed} navbar>
                            <ul className="navbar-nav flex-grow">

                                {this.state.access && <NavItem className="dropdown">
                                    <NavLink className="dropdown-toggle text-dark" data-toggle="dropdown">Админ панель</NavLink>
                                    <div className="dropdown-menu">
                                        <NavLink tag={Link} className="dropdown-item text-dark" to={AppPagePaths.ProductCreate}>Новый продукт</NavLink>
                                    </div>
                                </NavItem>}

                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/">На главную</NavLink>
                                </NavItem>
                                <LoginMenu>
                                </LoginMenu>
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}

