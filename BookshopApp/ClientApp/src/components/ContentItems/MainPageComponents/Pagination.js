import React, { Component } from 'react';

export class Pagination extends Component {

    constructor(props) {
        super(props);

    }

    render() {
        let visibleNumber = [1, 2, 3];


        return (
            <nav aria-label="Page navigation" className="mt-3">
                <ul className="pagination">
                    <li className="page-item">
                        <a className="page-link" href="#" aria-label="Previous">
                            <span aria-hidden="true">&laquo;</span>
                        </a>
                    </li>
                    <li className="page-item"><button className="page-link">{visibleNumber[0]}</button></li>
                    <li className="page-item"><a className="page-link" href="#">{visibleNumber[1]}</a></li>
                    <li className="page-item"><a className="page-link" href="#">{visibleNumber[2]}</a></li>
                    <li className="page-item">
                        <a className="page-link" href="#" aria-label="Next">
                            <span aria-hidden="true">&raquo;</span>
                        </a>
                    </li>
                </ul>
            </nav>);
    }
}