import React, { useEffect } from 'react';
import { Form, Button, Divider, Radio } from 'antd';
import { connect, Dispatch, WorkFlowListStateType, history } from 'umi';
import { PostFinancialFormStateType } from '../../model';
import styles from './index.less';
import { IWorkFlow } from '@/models/entity';

const formItemLayout = {
  labelCol: {
    span: 5,
  },
  wrapperCol: {
    span: 19,
  },
};
interface Step1Props {
  workflowid?: number;
  workFlowList: IWorkFlow[];
  dispatch: Dispatch;
}

const Step1: React.FC<Step1Props> = (props) => {
  useEffect(() => {
    dispatch({
      type: 'workFlowList/fetch',
    })
  }, [1]);
  const { dispatch, workFlowList, workflowid } = props;
  const [form] = Form.useForm();
  const { validateFields } = form;
  const onValidateForm = async () => {
    const values = await validateFields();
    console.log(values);
    if(values.wid === undefined){
      return;
    }
    if (dispatch) {
      dispatch({
        type: 'postFinancialForm/saveStepFormData',
        payload: values,
      });
      dispatch({
        type: 'postFinancialForm/saveCurrentStep',
        payload: 'input',
      });
    }
  };
  return (
    <>
      <Form
        {...formItemLayout}
        form={form}
        layout="horizontal"
        className={styles.stepForm}
        initialValues={{wid: workflowid}}
        hideRequiredMark
      >
        <Form.Item name={'wid'}>
          <Radio.Group>
            {workFlowList.map(u => (
              <Radio key={u.id} 
              value={u.id} 
              checked={u.id === workflowid}
              style={{
                display: 'block',
                height: '30px',
                fontSize: 15
              }}>{u.description}</Radio>
            ))}
          </Radio.Group>
        </Form.Item>
        <Button type="primary" onClick={onValidateForm}>
          下一步
        </Button>
      </Form>
      <Divider style={{ margin: '40px 0 24px' }} />
      <div className={styles.desc}>
        <h3>说明</h3>
        <p>
          还没有通过审批的申请不会显示在本页面，请前往<a onClick={()=>{history.push('/my/workflow')}}>这个地址</a>查看完整申请列表。
        </p>
      </div>
    </>
  );
};

export default connect(({
  postFinancialForm,
  workFlowList
}: {
  postFinancialForm: PostFinancialFormStateType;
  workFlowList: WorkFlowListStateType;
}) => ({
  workflowid: postFinancialForm.formdata?.wid,
  workFlowList: workFlowList.list.filter(u => u.state === 'securityOk') || []
}))(Step1);
