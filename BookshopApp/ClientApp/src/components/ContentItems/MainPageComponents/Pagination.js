import React, { Component } from 'react';

export class Pagination extends Component {

    constructor(props) {
        super(props);

        this.handlerButtonClick = this.handlerButtonClick.bind(this);
    }

    configureButtonNumberInfo() {
        const curPage = this.props.curPage;
        const pageIsLast = this.props.pageIsLast;
        //if curPage == then will be visible 1 and 2
        if (curPage === 1) {
            //if curPage is last will be visible only 1
            if (pageIsLast) return [{
                active: 'active',
                number: curPage
            }];
            return [{
                active: 'active',
                number: curPage
            }, {
                active: '',
                number: curPage + 1
            }];
        }
        //if curPage is last will be visible curPage-1 and curPage. For example: last page is 10, then it return 9, 10
        if (pageIsLast) {
            return [{
                active: '',
                number: curPage - 1
            }, {
                active: 'active',
                number: curPage
            }];
        }

        return [{
            active: '',
            number: curPage - 1
        }, {
            active: 'active',
            number: curPage
        }, {
            active: '',
            number: curPage + 1
        }];
    }

    handlerButtonClick(page) {
        if (this.props.curPage === page || page < 1 || this.props.pageIsLast && this.props.curPage < page)
            return;
        this.props.changePage(page);
    }

    render() {
        const buttonNumberInfo = this.configureButtonNumberInfo();

        return (
            <nav aria-label="Page navigation" className="mt-3">
                <ul className="pagination">
                    <li className="page-item">
                        <button className="page-link" aria-label="Previous" onClick={() => this.handlerButtonClick(this.props.curPage - 1)}>
                            <span >&laquo;</span>
                        </button>
                    </li>
                    {buttonNumberInfo.map((item, i) => <li key={i} className={"page-item " + item.active}><button className="page-link" onClick={() => this.handlerButtonClick(item.number)}>{item.number}</button></li>)}
                    <li className="page-item">
                        <button className="page-link" aria-label="Next" onClick={() => this.handlerButtonClick(this.props.curPage + 1)}>
                            <span >&raquo;</span>
                        </button>
                    </li>
                </ul>
            </nav>);
    }
}